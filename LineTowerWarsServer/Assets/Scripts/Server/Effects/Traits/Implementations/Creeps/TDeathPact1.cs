public class TDeathPact1 : Trait {
    public override TraitType Type => TraitType.DeathPact1;

    private bool ShouldRespawn { get; }

    public TDeathPact1(ServerEntity entity) : base(entity) {
        ShouldRespawn = (float) entity.HP / entity.MaxHealth > .95;
    }
    
    protected override void OnHolderDeath(ServerEntity entity) {
        if (ShouldRespawn) {
            TraitUtils.TriggerDeathPactRevive(
                entity,
                TraitConstants.DeathPact1ReviveHealthMultiplier,
                TraitConstants.DeathPact1ReviveDelay
            );
        }
    }
    
    public override bool RemovesCreepKillBounty => ShouldRespawn;
}