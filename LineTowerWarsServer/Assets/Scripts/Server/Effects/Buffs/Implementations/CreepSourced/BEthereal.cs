public class BEthereal : Buff_ProximityBased_Stacks {
    public override BuffType Type => BuffType.Ethereal;
    
    public BEthereal(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.EtherealArmorPerStack * Stacks;
}