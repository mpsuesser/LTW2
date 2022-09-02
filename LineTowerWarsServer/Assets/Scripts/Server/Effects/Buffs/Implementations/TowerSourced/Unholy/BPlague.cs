using System.Collections.Generic;

public class BPlague : Buff_DurationBased_StacksRefreshDuration {
    public override BuffType Type => BuffType.Plague;

    protected override double BaseDuration => TraitConstants.PlagueDuration;
    protected override int MaxStackCount => TraitConstants.PlagueMaxStacks;
    
    private bool IsSticky { get; set; }

    public BPlague(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) {
        // The plague should only spread if it's been made sticky, which happens
        // when an Ultimate Gravedigger attacks the unit with the buff or is the
        // one to apply it in the first place.
        IsSticky = false;
    }

    public override float DamageTakenMultiplier =>
        1 + TraitConstants.PlagueAdditionalDamageTakenMultiplierPerStack * Stacks;

    // Once the buff is sticky, it will spread to nearby creeps upon the affected
    // unit's death.
    public void MakeSticky() {
        if (IsSticky) {
            return;
        }
        
        OnAffectedUnitDied += SpreadUnendingPlague;
        IsSticky = true;
    }

    private void SpreadUnendingPlague(Buff _b) {
        HashSet<ServerEntity> creepsWithinSpreadRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                AffectedEntity,
                TraitConstants.AdvancedPlagueSpreadRadius,
                new CreepEntityFilter()
            );

        int numPerCreep = Stacks / creepsWithinSpreadRange.Count;
        int carryover = Stacks % creepsWithinSpreadRange.Count;

        foreach (ServerEntity creep in creepsWithinSpreadRange) {
            int numStacksToApply = numPerCreep + carryover;
            BuffFactory.ApplyBuff(
                BuffType.UnendingPlague,
                creep,
                null,
                numStacksToApply
            );
            
            carryover = 0;
        }
    }
}