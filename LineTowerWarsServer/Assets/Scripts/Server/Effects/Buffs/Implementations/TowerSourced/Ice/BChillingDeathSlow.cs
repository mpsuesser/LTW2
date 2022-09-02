public class BChillingDeathSlow : Buff_Static_Stacks {
    public override BuffType Type => BuffType.ChillingDeathSlow;

    protected override int MaxStackCount => TraitConstants.ChillingDeathMaxStacks;

    public BChillingDeathSlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        OnStacksUpdated += CheckForFrostbiteApplicationCondition;
    }

    private void CheckForFrostbiteApplicationCondition(Buff _b) {
        if (Stacks != MaxStackCount) {
            return;
        }

        BuffFactory.ApplyBuff(
            BuffType.Frostbite,
            AffectedEntity,
            AffectedEntity
        );

        OnStacksUpdated -= CheckForFrostbiteApplicationCondition;
    }

    public override float MovementSpeedMultiplier =>
        1 - TraitConstants.ChillingDeathMovementSpeedReductionMultiplierPerStack * Stacks;
}