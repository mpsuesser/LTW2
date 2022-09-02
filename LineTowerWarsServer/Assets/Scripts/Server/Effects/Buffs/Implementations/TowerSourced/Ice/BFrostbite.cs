public class BFrostbite : Buff_Static_NoStacks {
    public override BuffType Type => BuffType.Frostbite;
    
    public BFrostbite(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        // TODO: Add another effect. Increasing chill duration is lame and ambiguous.
    }

    public override float HealingReceivedMultiplier => TraitConstants.FrostbiteHealingReceivedMultiplier;
}