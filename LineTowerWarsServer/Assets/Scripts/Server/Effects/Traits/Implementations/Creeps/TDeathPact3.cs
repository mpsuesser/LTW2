public class TDeathPact3 : Trait {
    public override TraitType Type => TraitType.DeathPact3;

    private bool ShouldRespawn { get; }

    public TDeathPact3(ServerEntity entity) : base(entity) {
        ShouldRespawn = (float) entity.HP / entity.MaxHealth > .95;
    }
    
    protected override void OnHolderDeath(ServerEntity entity) {
        if (ShouldRespawn) {
            TraitUtils.TriggerDeathPactRevive(
                entity,
                TraitConstants.DeathPact3ReviveHealthMultiplier,
                TraitConstants.DeathPact3ReviveDelay
            );
        }
    }
    
    public override bool RemovesCreepKillBounty => ShouldRespawn;
}