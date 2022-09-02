public class TBasicSpellResistance : Trait {
    public override TraitType Type => TraitType.BasicSpellResistance;

    public TBasicSpellResistance(ServerEntity entity) : base(entity) { }

    public override bool IsImmuneToSlowEffects => true;
    public override float HarmfulEffectDurationMultiplier =>
        TraitConstants.BasicSpellResistanceHarmfulSpellEffectsDurationMultiplier;
}