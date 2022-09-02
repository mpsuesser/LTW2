public class TReactiveArmor : Trait {
    public override TraitType Type => TraitType.ReactiveArmor;

    public TReactiveArmor(ServerEntity entity) : base(entity) { }
    
    public override bool HasPostMultiplierDamageTakenAdjustment => true;
    public override void PostMultiplierDamageTakenAdjustment(
        ServerEntity damageDealer,
        ref double damageAmount
    ) {
        if (damageAmount <= TraitConstants.ReactiveArmorFirstThreshold) {
            return;
        }

        if (damageAmount <= TraitConstants.ReactiveArmorSecondThreshold) {
            double chunkToReduce = damageAmount - TraitConstants.ReactiveArmorFirstThreshold;
            damageAmount =
                TraitConstants.ReactiveArmorFirstThreshold
                + chunkToReduce * TraitConstants.ReactiveArmorFirstThresholdMultiplier;
            return;
        }
        
        // At this point we know the damage amount exceeds the second threshold, so...
        double baseChunk = TraitConstants.ReactiveArmorFirstThreshold;
        double middleChunk = (
            TraitConstants.ReactiveArmorSecondThreshold
            - TraitConstants.ReactiveArmorFirstThreshold
        ) * TraitConstants.ReactiveArmorFirstThresholdMultiplier;
        double endChunk = (
            damageAmount - TraitConstants.ReactiveArmorSecondThreshold
        ) * TraitConstants.ReactiveArmorSecondThresholdMultiplier;
        
        damageAmount = baseChunk + middleChunk + endChunk;
    }
}