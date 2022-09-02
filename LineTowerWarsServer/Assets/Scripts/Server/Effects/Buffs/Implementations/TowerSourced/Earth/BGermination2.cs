public class BGermination2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.Germination2;
    
    protected override int MaxStackCount =>
        TraitConstants.Germination2MaxStacks;

    private int AttacksRemaining { get; set; }

    public BGermination2(
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
        AttacksRemaining = TraitConstants.Germination2AttackCount;
    }

    private void UpdateAttacksRemaining(ServerEntity attacker, ServerEntity target) {
        AttacksRemaining--;
        if (AttacksRemaining <= 0) {
            Purge();
        }
    }

    public override float DamageDoneMultiplier =>
        1 + TraitConstants.Germination2AdditionalDamageDealtMultiplierPerStack * Stacks;
}