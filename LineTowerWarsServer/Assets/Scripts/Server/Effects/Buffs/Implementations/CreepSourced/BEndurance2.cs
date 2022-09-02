public class BEndurance2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Endurance2;
    
    public BEndurance2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => (float) TraitConstants.EnduranceAura2SpeedMultiplier;
    public override float AttackSpeedMultiplier => (float) TraitConstants.EnduranceAura2SpeedMultiplier;
}