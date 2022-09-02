using UnityEngine;

public class BVolcanicExposure : Buff_Static_Stacks {
    public override BuffType Type => BuffType.VolcanicExposure;
    
    public BVolcanicExposure(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    // TODO: Update this to use the % max formula
    public override float ArmorReductionNotBelowZero =>
        -TraitConstants.VolcanicEruptionMaxArmorReduction * Stacks;
}