public class BTurbulentWeather2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.TurbulentWeather2;
    
    public BTurbulentWeather2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => TraitConstants.TurbulentWeather2MovementSpeedMultiplier;
}