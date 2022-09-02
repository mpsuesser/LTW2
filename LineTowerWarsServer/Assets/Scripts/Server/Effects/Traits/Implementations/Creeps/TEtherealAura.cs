public class TEtherealAura : Trait {
    public override TraitType Type => TraitType.EtherealAura;

    public TEtherealAura(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EtherealAura.Create(entity);
    }
}