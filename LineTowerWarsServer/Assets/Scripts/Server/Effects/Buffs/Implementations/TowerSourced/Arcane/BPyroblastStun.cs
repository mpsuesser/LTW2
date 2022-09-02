public class BPyroblastStun : Buff_Stun {
    public override BuffType Type => BuffType.PyroblastStun;

    protected override double BaseDuration => TraitConstants.KirinTorMasteryPyroblastStunDuration;
    
    public BPyroblastStun(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
}