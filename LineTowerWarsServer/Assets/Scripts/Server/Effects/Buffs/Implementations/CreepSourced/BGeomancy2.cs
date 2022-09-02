public class BGeomancy2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Geomancy2;
    
    public BGeomancy2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionEffectEffectivenessMultiplier =>
        TraitConstants.Geomancy2ArmorReductionEffectivenessMultiplier;
}