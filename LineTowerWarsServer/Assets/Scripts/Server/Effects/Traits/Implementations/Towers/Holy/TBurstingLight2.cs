public class TBurstingLight2 : Trait {
    public override TraitType Type => TraitType.BurstingLight2;

    public TBurstingLight2(ServerEntity entity) : base(entity) { }

    public override int AdditionalAttackTargets =>
        TraitConstants.BurstingLight2AdditionalTargets;
}