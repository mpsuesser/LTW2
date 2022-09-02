public class TPressuringWater1 : Trait {
    public override TraitType Type => TraitType.PressuringWater1;

    public TPressuringWater1(ServerEntity entity) : base(entity) { }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;

    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damage
    ) {
        // TODO: Sloppy, get rid of this
        if (!(E is ServerTower T)) {
            return;
        }
        
        damage *= TraitUtils.GetDamageMultiplierBasedOnRangeFromSource(
            E,
            target,
            T.Threat.GameRange,
            TraitConstants.PressuringWater1MaxDamageDealtMultiplier
        );
    }
}