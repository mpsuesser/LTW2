public class TFocusedLightning2 : Trait {
    public override TraitType Type => TraitType.FocusedLightning2;

    private ServerEntity PreviousTarget { get; set; }
    private int ConsecutiveStrikesOnSameTarget { get; set; }

    public TFocusedLightning2(ServerEntity entity) : base(entity) {
        PreviousTarget = null;
        ConsecutiveStrikesOnSameTarget = 0;
    }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        UpdateConsecutiveStrikesCount(target);

        damageAmount *= 1 + (
            ConsecutiveStrikesOnSameTarget
            * TraitConstants.FocusedLightning2DamageIncreasePerHit
        );
    }

    private void UpdateConsecutiveStrikesCount(ServerEntity target) {
        if (target != PreviousTarget) {
            ConsecutiveStrikesOnSameTarget = 0;
            PreviousTarget = target;
            return;
        }

        ConsecutiveStrikesOnSameTarget++;
    }
}