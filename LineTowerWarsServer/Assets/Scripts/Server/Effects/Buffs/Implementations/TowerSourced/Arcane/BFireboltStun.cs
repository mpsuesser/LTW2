public class BFireboltStun : Buff_Stun {
    public override BuffType Type => BuffType.FireboltStun;

    protected override double BaseDuration => TraitConstants.Spellcaster1FireboltStunDuration;
    
    public BFireboltStun(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
}