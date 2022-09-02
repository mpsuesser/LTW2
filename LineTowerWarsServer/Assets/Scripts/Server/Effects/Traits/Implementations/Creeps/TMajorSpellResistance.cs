public class TMajorSpellResistance : Trait {
    public override TraitType Type => TraitType.MajorSpellResistance;

    public TMajorSpellResistance(ServerEntity entity) : base(entity) { }

    public override bool IsImmuneToSlowEffects => true;
    public override float HarmfulEffectDurationMultiplier =>
        TraitConstants.MajorSpellResistanceHarmfulSpellEffectsDurationMultiplier;
}