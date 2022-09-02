using System.Security.Cryptography;

public class BHardenedSkin : Buff_Static_Stacks {
    public override BuffType Type => BuffType.HardenedSkin;

    public BHardenedSkin(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity,
        TraitConstants.HardenedSkinStartingStacks
    ) {
        affectedEntity.OnDamageTaken += CheckDamageEventForStackRemoval;
    }

    private void CheckDamageEventForStackRemoval(
        ServerEntity damageDealer,
        double damageAmount,
        DamageType damageType,
        DamageSourceType damageSourceType
    ) {
        if (
            damageType == DamageType.Physical
            && damageAmount > TraitConstants.HardenedSkinMinimumDamageToRemoveStack
        ) {
            RemoveStack();
        }
    }

    public override float ArmorDiff =>
        TraitConstants.HardenedSkinArmorPerStack * Stacks;
}