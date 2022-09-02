public class BEssenceOfPowerStun : Buff_Stun {
    public override BuffType Type => BuffType.EssenceOfPowerStun;

    protected override double BaseDuration => TraitConstants.EssenceOfPowerStunDuration;
    
    public BEssenceOfPowerStun(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
}