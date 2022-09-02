public class BUnendingPlague : Buff_Static_Stacks {
    public override BuffType Type => BuffType.UnendingPlague;

    public BUnendingPlague(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity,
        int startingStackCount
    ) : base(
        affectedEntity,
        appliedByEntity,
        startingStackCount
    ) { }

    public override float DamageTakenMultiplier =>
        1 + TraitConstants.PlagueAdditionalDamageTakenMultiplierPerStack * Stacks;
}