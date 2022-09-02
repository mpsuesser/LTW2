public class TOverwhelmingImpact2 : Trait {
    public override TraitType Type => TraitType.OverwhelmingImpact2;

    public TOverwhelmingImpact2(ServerEntity entity) : base(entity) {
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
            TraitConstants.OverwhelmingImpact2BurnDamagePerSecond,
            DamageSourceType.OverwhelmingImpact2MeteorBurn,
            TraitConstants.OverwhelmingImpact2MeteorBurnTicks,
            false
        );
    }
}