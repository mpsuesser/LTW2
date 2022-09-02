public class BAncient1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Ancient1;
    
    public BAncient1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float SlowEffectEffectivenessMultiplier =>
        TraitConstants.Ancient1NegativeMovementEffectivenessMultiplier;
}