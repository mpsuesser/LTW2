public class BLethalPreparation : Buff_Static_Stacks {
    public override BuffType Type => BuffType.LethalPreparation;
    
    protected override int MaxStackCount =>
        TraitConstants.LethalPreparationMaxStacks;

    private int AttacksRemaining { get; set; }

    public BLethalPreparation(
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
        AttacksRemaining = TraitConstants.LethalPreparationAttackCount;
    }

    private void UpdateAttacksRemaining(ServerEntity attacker, ServerEntity target) {
        AttacksRemaining--;
        if (AttacksRemaining <= 0) {
            Purge();
        }
    }

    public override float DamageDoneMultiplier =>
        1 + TraitConstants.LethalPreparationAdditionalDamageDealtMultiplierPerStack * Stacks;
}