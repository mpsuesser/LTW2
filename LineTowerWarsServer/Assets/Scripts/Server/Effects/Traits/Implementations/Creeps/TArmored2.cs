public class TArmored2 : Trait {
    public override TraitType Type => TraitType.Armored2;

    public TArmored2(ServerEntity entity) : base(entity) { }

    public override float PhysicalSplashDamageTakenMultiplier =>
        TraitConstants.Armored2PhysicalSplashDamageTakenMultiplier;
}