public class BTurbulentWeather3 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.TurbulentWeather3;
    
    public BTurbulentWeather3(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => TraitConstants.TurbulentWeather3MovementSpeedMultiplier;
}