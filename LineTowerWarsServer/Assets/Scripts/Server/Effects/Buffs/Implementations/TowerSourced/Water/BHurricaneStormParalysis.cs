public class BHurricaneStormParalysis : Buff_Stun {
    public override BuffType Type => BuffType.HurricaneStormParalysis;

    protected override double BaseDuration => TraitConstants.HurricaneStormParalyzationDuration;
    
    public BHurricaneStormParalysis(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }
}