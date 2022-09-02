public class TChillingDeath : Trait {
    public override TraitType Type => TraitType.ChillingDeath;

    public TChillingDeath(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        ProximityBuffApplicator_ChillingDeathAura.Create(entity);

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.ChillingDeathSlow,
            target,
            attacker
        );
    }
}