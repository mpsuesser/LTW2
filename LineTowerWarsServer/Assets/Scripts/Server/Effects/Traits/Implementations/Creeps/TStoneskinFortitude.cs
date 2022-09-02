public class TStoneskinFortitude : Trait {
    public override TraitType Type => TraitType.StoneskinFortitude;

    public TStoneskinFortitude(ServerEntity entity) : base(entity) { }

    public override bool IsImmuneToSlowEffects => true;
}