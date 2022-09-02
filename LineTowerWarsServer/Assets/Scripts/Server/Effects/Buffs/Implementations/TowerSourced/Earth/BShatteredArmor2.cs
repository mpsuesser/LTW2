public class BShatteredArmor2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.ShatteredArmor2;

    protected override double BaseDuration => TraitConstants.ShatterArmor2Duration;
    
    public BShatteredArmor2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        -TraitConstants.ShatterArmor2ArmorReduction;
}