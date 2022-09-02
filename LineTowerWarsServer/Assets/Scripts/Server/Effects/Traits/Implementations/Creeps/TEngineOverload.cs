using System.Collections.Generic;
using System.Linq;

public class TEngineOverload : Trait {
    public override TraitType Type => TraitType.EngineOverload;

    private Stack<float> RemainingHealthThresholdTriggers { get; }

    public TEngineOverload(ServerEntity entity) : base(entity) {
        List<float> guaranteedSortedHealthRatioThresholds = new List<float>(
            TraitConstants.EngineOverloadHealthPercentageTriggers
        );
        guaranteedSortedHealthRatioThresholds.Sort();
        
        RemainingHealthThresholdTriggers = new Stack<float>();
        foreach (float threshold in guaranteedSortedHealthRatioThresholds) {
            RemainingHealthThresholdTriggers.Push(threshold);
        }
        
        entity.OnDamageTaken += CheckForStaticBurstCondition;
    }

    private void CheckForStaticBurstCondition(
        ServerEntity entity,
        double _damage,
        DamageType _damageType,
        DamageSourceType _damageSourceType
    ) {
        if (RemainingHealthThresholdTriggers.Count == 0) {
            entity.OnDamageTaken -= CheckForStaticBurstCondition;
            return;
        }

        if (E.HealthRatio < RemainingHealthThresholdTriggers.Peek()) {
            RemainingHealthThresholdTriggers.Pop();
            DoStaticBurst();
        }
    }

    private void DoStaticBurst() {
        HashSet<ServerEntity> towersInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.EngineOverloadRange,
                new TowerEntityFilter()
            );
        
        // TODO: Send event
        
        foreach (ServerEntity tower in towersInRange) {
            BuffFactory.ApplyBuff(
                BuffType.StaticOverload,
                tower,
                E
            );
        }
    }
}