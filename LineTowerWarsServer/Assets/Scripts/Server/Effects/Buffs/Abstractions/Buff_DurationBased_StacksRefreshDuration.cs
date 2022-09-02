using UnityEngine;

public abstract class Buff_DurationBased_StacksRefreshDuration : Buff_DurationBased {
    protected override bool IsStacking => true;
    
    private double _remainingDuration;

    protected Buff_DurationBased_StacksRefreshDuration(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount = 1
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) {
        _remainingDuration = AdjustedBaseDuration;
    }

    public override BuffTransitData PackageDataForTransit() {
        return new BuffTransitData(
            ID,
            Type,
            Stacks,
            true,
            AdjustedBaseDuration,
            _remainingDuration
        );
    }
    
    public override void AddStacksFrom(
        int additionalStackCount,
        ServerEntity appliedByEntity
    ) {
        AddStacks(additionalStackCount);

        _remainingDuration = AdjustedBaseDuration;
    }

    public override void Update() {
        _remainingDuration -= Time.deltaTime;
        if (_remainingDuration < 0) {
            Remove();
        }

        base.Update();
    }
}