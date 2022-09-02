public class BTurbulentWeather1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.TurbulentWeather1;
    
    public BTurbulentWeather1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier => TraitConstants.TurbulentWeather1MovementSpeedMultiplier;
}