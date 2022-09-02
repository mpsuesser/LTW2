public class BEndurance4 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Endurance4;
    
    public BEndurance4(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => (float) TraitConstants.EnduranceAura4SpeedMultiplier;
    public override float AttackSpeedMultiplier => (float) TraitConstants.EnduranceAura4SpeedMultiplier;
}