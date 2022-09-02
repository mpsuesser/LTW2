public class TChaoticVoid : Trait {
    public override TraitType Type => TraitType.ChaoticVoid;

    public TChaoticVoid(ServerEntity entity) : base(entity) {
        entity.OnDamageTaken += GainManaAndCheckForHeal;
    }

    private void GainManaAndCheckForHeal(
        ServerEntity entity,
        double _damage,
        DamageType _damageType,
        DamageSourceType _damageSourceType
    ) {
        entity.Status.GainMana(TraitConstants.ChaoticVoidManaPerDamageInstance);

        if (entity.MP == entity.MaxMana) {
            entity.Status.HealForAmount(
                entity.MaxHealth * TraitConstants.ChaoticVoidHealPercentageOfMaxHealth
            );
            
            entity.Status.DumpAllMana();
        }
    }
}