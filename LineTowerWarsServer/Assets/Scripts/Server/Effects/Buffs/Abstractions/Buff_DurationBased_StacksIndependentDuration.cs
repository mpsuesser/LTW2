using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff_DurationBased_StacksIndependentDuration : Buff_DurationBased {
    protected override bool IsStacking => true;

    private List<float> stackApplicationTimes;

    protected Buff_DurationBased_StacksIndependentDuration(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount = 1
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) {
        stackApplicationTimes = new List<float>();
    }
    
    public override BuffTransitData PackageDataForTransit() {
        float timeRemainingOfMostRecentApplication =
            (float)AdjustedBaseDuration - (Time.time - stackApplicationTimes[stackApplicationTimes.Count - 1]);
        
        return new BuffTransitData(
            ID,
            Type,
            Stacks,
            true,
            AdjustedBaseDuration,
            timeRemainingOfMostRecentApplication
        );
    }

    public override void AddStacksFrom(
        int additionalStackCount,
        ServerEntity appliedByEntity
    ) {
        for (int i = 0; i < additionalStackCount; i++) {
            stackApplicationTimes.Add(Time.time);
        }

        AddStacks(additionalStackCount);
    }

    public override void Update() {
        int expiredStacks = 0;
        for (int i = 0; i < stackApplicationTimes.Count; i++) {
            if (stackApplicationTimes[i] + AdjustedBaseDuration < Time.time) {
                expiredStacks++;
                stackApplicationTimes.RemoveAt(i);
            }
            else {
                break;
            }
        }
        
        RemoveStacks(expiredStacks);

        base.Update();
    }
}