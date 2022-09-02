using System.Collections.Generic;

public class BCorrupted2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.Corrupted2;

    protected override double BaseDuration => TraitConstants.FadingCorruption2Duration;

    public BCorrupted2(
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
                TraitConstants.FadingCorruption2Radius,
                new CreepEntityFilter()
            );
        
        foreach (ServerEntity creep in creepsWithinExplosionRange) {
            AffectedEntity.DealDamageTo(
                creep,
                TraitConstants.FadingCorruption2Damage,
                DamageType.Spell,
                DamageSourceType.Corrupted2Explosion
            );
        }
    }
}