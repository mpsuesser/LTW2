public class BEssenceOfFrost1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfFrost1;

    public BEssenceOfFrost1(
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
            BuffType.EssenceOfFrostSlow1,
            target,
            attacker
        );
    }
}