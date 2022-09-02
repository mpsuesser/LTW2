using System;
using System.Collections.Generic;

public class ClientElementalTechSystem : SingletonBehaviour<ClientElementalTechSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    public bool LaneFulfillsRequirementsForPurchase(Lane lane, ElementalTechType upgradeType) {
        return lane.Gold >= lane.TechCost
            && (
            !ElementalTech.Prerequisite.ContainsKey(upgradeType)
            || lane.HasTech(ElementalTech.Prerequisite[upgradeType])
        );
    }
}
