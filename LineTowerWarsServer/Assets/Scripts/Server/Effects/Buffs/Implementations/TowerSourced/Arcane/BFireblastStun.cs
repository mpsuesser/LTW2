public class BFireblastStun : Buff_Stun {
    public override BuffType Type => BuffType.FireblastStun;

    protected override double BaseDuration => TraitConstants.Spellcaster2FireblastStunDuration;
    
    public BFireblastStun(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
}