public class BAncient2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Ancient2;
    
    public BAncient2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float SlowEffectEffectivenessMultiplier =>
        TraitConstants.Ancient2NegativeMovementEffectivenessMultiplier;
}