public static class BuilderConstants {
    public const float BuilderMoveSpeed = 600;
    public const float DelayBetweenQueuedBuildCommands = 0.4f;
    
    // Attacks
    public const double BuilderAttackRange = 127;
    public static readonly (double, double) BuilderAttackDamageRange = (10, 10);
    public const double BuilderAttackCooldown = 3;
    public const AttackType BuilderAttackModifier = AttackType.Normal;
    public static readonly AttackDeliveryModifiers BuilderAttackDelivery =
        new InstantAttackDelivery {
            InitialAnimationDelay = 0
        };
}