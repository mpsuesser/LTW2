public class BEndurance1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Endurance1;
    
    public BEndurance1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => (float) TraitConstants.EnduranceAura1SpeedMultiplier;
    public override float AttackSpeedMultiplier => (float) TraitConstants.EnduranceAura1SpeedMultiplier;
}