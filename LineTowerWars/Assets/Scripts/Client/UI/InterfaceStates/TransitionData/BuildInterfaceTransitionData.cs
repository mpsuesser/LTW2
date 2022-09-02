public class BuildInterfaceTransitionData : InterfaceTransitionData {
    public TowerType Type { get; }

    public BuildInterfaceTransitionData(TowerType type) {
        Type = type;
    }
}