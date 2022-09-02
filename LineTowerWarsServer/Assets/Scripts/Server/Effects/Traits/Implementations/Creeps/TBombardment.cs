public class TBombardment : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Bombardment;

    public TBombardment(ServerEntity entity) : base(entity) {
        Ticker.Subscribe(this);
    }
    
    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() => TraitConstants.BombardmentRocketInterval;
    public void DoPeriodicThing() {
        try {
            ServerEntity tower =
                TraitUtils.GetRandomTowerWithinGameRangeOfEntity(
                    E,
                    TraitConstants.BombardmentRange
                );
            
            // TODO: Instead of instant damage, create a projectile and send to client
            E.DealDamageTo(
                tower,
                TraitConstants.BombardmentRocketDamage,
                DamageType.Spell,
                DamageSourceType.BombardmentRocket
            );
        }
        catch (NoNearbyTowersException) {}
    }
}