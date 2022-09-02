public class TGoblinEngineering : Trait {
    public override TraitType Type => TraitType.GoblinEngineering;

    public TGoblinEngineering(ServerEntity entity) : base(entity) { }

    public override float MovementSpeedMultiplierFloor =>
        TraitConstants.GoblinEngineeringMinBaseMovementSpeed;
    
    public override float HarmfulEffectDurationMultiplier =>
        TraitConstants.GoblinEngineeringHarmfulEffectsDurationMultiplier;
}