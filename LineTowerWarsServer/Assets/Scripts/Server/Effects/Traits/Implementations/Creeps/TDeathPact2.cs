public class TDeathPact2 : Trait {
    public override TraitType Type => TraitType.DeathPact2;

    private bool ShouldRespawn { get; }

    public TDeathPact2(ServerEntity entity) : base(entity) {
        ShouldRespawn = (float) entity.HP / entity.MaxHealth > .95;
    }
    
    protected override void OnHolderDeath(ServerEntity entity) {
        if (ShouldRespawn) {
            TraitUtils.TriggerDeathPactRevive(
                entity,
                TraitConstants.DeathPact2ReviveHealthMultiplier,
                TraitConstants.DeathPact2ReviveDelay
            );
        }
    }
    
    public override bool RemovesCreepKillBounty => ShouldRespawn;
}