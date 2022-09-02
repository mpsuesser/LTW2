public class BEndurance3 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Endurance3;
    
    public BEndurance3(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => (float) TraitConstants.EnduranceAura3SpeedMultiplier;
    public override float AttackSpeedMultiplier => (float) TraitConstants.EnduranceAura3SpeedMultiplier;
}