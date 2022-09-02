public class TTurbulentWeather1 : Trait {
    public override TraitType Type => TraitType.TurbulentWeather1;

    public TTurbulentWeather1(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_TurbulentWeather1.Create(entity);
    }
}