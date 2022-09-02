public class BEssenceOfDarkness2 : Buff_ProximityBased_Stacks {
    public override BuffType Type => BuffType.EssenceOfDarkness2;
    
    public BEssenceOfDarkness2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier => 1 + TraitConstants.EssenceOfDarkness2AdditionalDamageDealtPerStack *
        Stacks;
}