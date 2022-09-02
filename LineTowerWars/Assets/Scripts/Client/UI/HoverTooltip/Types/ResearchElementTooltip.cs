using System.Collections.Generic;

public class ResearchElementTooltip : Tooltip {
    public string Title { get; private set; }
    public int GoldCost { get; private set; }
    public List<TowerType> AssociatedTowers { get; private set; }

    public ResearchElementTooltip(ElementalTechType elementalTechType) : base() {
        Title = $"Research: {ElementalTechConstants.DisplayName[elementalTechType]}";
        GoldCost = ClientLaneTracker.Singleton.MyLane.TechCost;
        AssociatedTowers = TowerUpgrades.Singleton.GetAllTowersAssociatedWithElementalTechType(elementalTechType);
    }
}