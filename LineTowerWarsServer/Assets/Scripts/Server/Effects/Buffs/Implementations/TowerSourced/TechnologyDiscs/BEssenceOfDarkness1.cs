public class BEssenceOfDarkness1 : Buff_ProximityBased_Stacks {
    public override BuffType Type => BuffType.EssenceOfDarkness1;
    
    public BEssenceOfDarkness1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier => 1 + TraitConstants.EssenceOfDarkness1AdditionalDamageDealtPerStack *
                                                     Stacks;
}