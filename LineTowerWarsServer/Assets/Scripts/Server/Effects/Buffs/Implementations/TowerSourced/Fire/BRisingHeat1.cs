public class BRisingHeat1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.RisingHeat1;
    protected override int MaxStackCount =>
        TraitConstants.RisingHeat1StackResetThreshold;

    private int PrevStacks { get; set; }

    public BRisingHeat1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        PrevStacks = 1;
        
        OnStacksUpdated += CheckForStackCycle;
    }

    private void CheckForStackCycle(Buff _b) {
        if (PrevStacks == MaxStackCount) {
            Purge();
            return;
        }

        PrevStacks = Stacks;
    }

    public override float AttackSpeedMultiplier =>
        1 + TraitConstants.RisingHeat1AttackSpeedIncreasePerStack * Stacks;
}