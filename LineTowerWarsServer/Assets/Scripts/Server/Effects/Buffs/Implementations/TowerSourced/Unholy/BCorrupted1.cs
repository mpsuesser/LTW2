using System.Collections.Generic;

public class BCorrupted1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.Corrupted1;

    protected override double BaseDuration => TraitConstants.FadingCorruption1Duration;

    public BCorrupted1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        OnAffectedUnitDied += Explode;
    }

    private void Explode(Buff _b) {
        HashSet<ServerEntity> creepsWithinExplosionRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                AffectedEntity,
                TraitConstants.FadingCorruption1Radius,
                new CreepEntityFilter()
            );
        
        foreach (ServerEntity creep in creepsWithinExplosionRange) {
            AffectedEntity.DealDamageTo(
                creep,
                TraitConstants.FadingCorruption1Damage,
                DamageType.Spell,
                DamageSourceType.Corrupted1Explosion
            );
        }
    }
}