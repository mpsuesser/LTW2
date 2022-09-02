using UnityEngine;

public abstract class Buff_DurationBased_NoStacks : Buff_DurationBased {
    protected override bool IsStacking => false;

    private double _remainingDuration;

    protected Buff_DurationBased_NoStacks(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity,
        1
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