public class TTurbulentWeather2 : Trait {
    public override TraitType Type => TraitType.TurbulentWeather2;

    public TTurbulentWeather2(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_TurbulentWeather2.Create(entity);
    }
}