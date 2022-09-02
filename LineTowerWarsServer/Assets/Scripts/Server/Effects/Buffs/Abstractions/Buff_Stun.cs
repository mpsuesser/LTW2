public abstract class Buff_Stun : Buff_DurationBased_NoStacks {
    protected Buff_Stun(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
    
    public override bool IsStunned => true;
}