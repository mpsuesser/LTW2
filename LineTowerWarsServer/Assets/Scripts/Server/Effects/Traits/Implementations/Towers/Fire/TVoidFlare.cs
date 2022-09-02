using UnityEngine;

public class TVoidFlare : Trait {
    public override TraitType Type => TraitType.VoidFlare;

    public TVoidFlare(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += CreateGroundBurn;
    }

    private static void CreateGroundBurn(
        ServerEntity attacker,
        ServerEntity target
    ) {
        float damagePerTick = Mathf.Lerp(
            TraitConstants.VoidFlareMinDamagePerSecond,
            TraitConstants.VoidFlareMaxDamagePerSecond,
            (float) attacker.ManaRatio
        );
        
        MeteoricGroundBurner.Create(
            target.transform.position,
            attacker,
            damagePerTick,
            DamageSourceType.VoidFlareMeteorBurn,
            TraitConstants.VoidFlareMeteorBurnTicks,
            true
        );
        
        attacker.Status.DumpAllMana();
    }

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.VoidFlareManaRegenPerSecondDiff;
}