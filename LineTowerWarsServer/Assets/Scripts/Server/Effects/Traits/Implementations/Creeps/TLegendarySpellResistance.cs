public class TLegendarySpellResistance : Trait {
    public override TraitType Type => TraitType.LegendarySpellResistance;

    public TLegendarySpellResistance(ServerEntity entity) : base(entity) { }

    public override bool IsImmuneToSlowEffects => true;
    public override bool IsImmuneToHarmfulSpellEffects => true;
}