public class TWarStance : Trait {
    public override TraitType Type => TraitType.WarStance;

    public TWarStance(ServerEntity entity) : base(entity) {
        entity.OnDamageTaken += CheckForWarStanceCondition;
    }

    private void CheckForWarStanceCondition(
        ServerEntity entity,
        double _damage,
        DamageType _damageType,
        DamageSourceType _damageSourceType
    ) {
        if (E.HealthRatio > TraitConstants.WarStanceActivationHealthRatio) {
            return;
        }

        BuffFactory.ApplyBuff(
            BuffType.WarStance,
            E,
            E
        );
        ProximityBuffApplicator_WarCryAura.Create(E);
        
        entity.OnDamageTaken -= CheckForWarStanceCondition;
    }
}