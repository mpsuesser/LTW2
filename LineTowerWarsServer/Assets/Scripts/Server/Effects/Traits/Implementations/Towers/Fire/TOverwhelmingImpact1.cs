public class TOverwhelmingImpact1 : Trait {
    public override TraitType Type => TraitType.OverwhelmingImpact1;

    public TOverwhelmingImpact1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += CreateGroundBurn;
    }

    private static void CreateGroundBurn(
        ServerEntity attacker,
        ServerEntity target
    ) {
        MeteoricGroundBurner.Create(
            target.transform.position,
            attacker,
            TraitConstants.OverwhelmingImpact1BurnDamagePerSecond,
            DamageSourceType.OverwhelmingImpact1MeteorBurn,
            TraitConstants.OverwhelmingImpact1MeteorBurnTicks,
            false
        );
    }
}