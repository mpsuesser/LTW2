public class BWarCry : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.WarCry;
    
    public BWarCry(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier => TraitConstants.WarCryDamageDealtMultiplier;
}