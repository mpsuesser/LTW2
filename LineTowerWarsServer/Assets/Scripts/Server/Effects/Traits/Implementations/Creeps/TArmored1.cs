public class TArmored1 : Trait {
    public override TraitType Type => TraitType.Armored1;

    public TArmored1(ServerEntity entity) : base(entity) { }

    public override float PhysicalSplashDamageTakenMultiplier =>
        TraitConstants.Armored1PhysicalSplashDamageTakenMultiplier;
}