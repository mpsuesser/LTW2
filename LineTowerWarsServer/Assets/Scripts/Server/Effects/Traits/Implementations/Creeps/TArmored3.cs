public class TArmored3 : Trait {
    public override TraitType Type => TraitType.Armored3;

    public TArmored3(ServerEntity entity) : base(entity) { }

    public override float PhysicalSplashDamageTakenMultiplier =>
        TraitConstants.Armored3PhysicalSplashDamageTakenMultiplier;
}