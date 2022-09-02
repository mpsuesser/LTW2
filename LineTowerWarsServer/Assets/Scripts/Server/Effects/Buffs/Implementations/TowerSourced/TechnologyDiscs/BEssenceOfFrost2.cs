public class BEssenceOfFrost2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfFrost2;

    public BEssenceOfFrost2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        if (!(affectedEntity is IAttacker attacker)) {
            return;
        }

        attacker.Attack.OnAttackLandedPost += ApplySlowDebuff;
    }

    private void ApplySlowDebuff(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.EssenceOfFrostSlow2,
            target,
            attacker
        );
    }
}