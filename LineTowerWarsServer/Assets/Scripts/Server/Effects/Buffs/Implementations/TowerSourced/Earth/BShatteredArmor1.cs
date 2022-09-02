public class BShatteredArmor1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.ShatteredArmor1;

    protected override double BaseDuration => TraitConstants.ShatterArmor1Duration;
    
    public BShatteredArmor1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        -TraitConstants.ShatterArmor1ArmorReduction;
}