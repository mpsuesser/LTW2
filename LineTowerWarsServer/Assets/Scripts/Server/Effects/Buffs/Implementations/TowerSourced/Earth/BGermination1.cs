public class BGermination1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.Germination1;

    protected override int MaxStackCount =>
        TraitConstants.Germination1MaxStacks;

    private int AttacksRemaining { get; set; }

    public BGermination1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        RefreshAttacksRemaining();
        OnStacksUpdated += RefreshAttacksRemaining;

        if (!(affectedEntity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += UpdateAttacksRemaining;
    }

    private void RefreshAttacksRemaining(Buff _b = null) {
        AttacksRemaining = TraitConstants.Germination1AttackCount;
    }

    private void UpdateAttacksRemaining(ServerEntity attacker, ServerEntity target) {
        AttacksRemaining--;
        if (AttacksRemaining <= 0) {
            Purge();
        }
    }

    public override float DamageDoneMultiplier =>
        1 + TraitConstants.Germination1AdditionalDamageDealtMultiplierPerStack * Stacks;
}