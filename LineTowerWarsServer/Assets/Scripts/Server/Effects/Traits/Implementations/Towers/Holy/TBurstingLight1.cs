public class TBurstingLight1 : Trait {
    public override TraitType Type => TraitType.BurstingLight1;

    public TBurstingLight1(ServerEntity entity) : base(entity) { }

    public override int AdditionalAttackTargets =>
        TraitConstants.BurstingLight1AdditionalTargets;
}