public class TFlying : Trait {
    public override TraitType Type => TraitType.Flying;

    public TFlying(ServerEntity entity) : base(entity) { }

    // TODO: Not sure this is needed
}