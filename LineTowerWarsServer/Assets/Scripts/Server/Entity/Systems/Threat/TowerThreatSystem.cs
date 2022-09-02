public class TowerThreatSystem : ThreatSystem {
    public TowerThreatSystem(ServerTower t) : base(
        t,
        (float) TowerConstants.Range[t.Type]
    ) { }
}