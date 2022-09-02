public class ElementalTechSystem : SingletonBehaviour<ElementalTechSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        ServerEventBus.OnRequestPurchaseElementalTech += ProcessPurchaseRequest;
    }

    private void OnDestroy() {
        ServerEventBus.OnRequestPurchaseElementalTech -= ProcessPurchaseRequest;
    }

    private static void ProcessPurchaseRequest(Lane lane, ElementalTechType techType) {
        if (lane.Techs.Contains(techType)) {
            return;
        }

        if (lane.Gold < lane.TechCost) {
            return;
        }

        if (ElementalTech.Prerequisite.ContainsKey(techType) && !lane.Techs.Contains(ElementalTech.Prerequisite[techType])) {
            return;
        }

        lane.DeductGold(lane.TechCost);
        lane.SetTechCost(lane.TechCost * 2);
        lane.AddTech(techType);
    }
}
