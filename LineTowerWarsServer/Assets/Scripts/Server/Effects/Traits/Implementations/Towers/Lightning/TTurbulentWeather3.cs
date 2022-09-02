public class TTurbulentWeather3 : Trait {
    public override TraitType Type => TraitType.TurbulentWeather3;

    public TTurbulentWeather3(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_TurbulentWeather3.Create(entity);
    }
}