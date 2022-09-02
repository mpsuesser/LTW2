public class BGeomancy1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Geomancy1;
    
    public BGeomancy1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionEffectEffectivenessMultiplier =>
        TraitConstants.Geomancy1ArmorReductionEffectivenessMultiplier;
}