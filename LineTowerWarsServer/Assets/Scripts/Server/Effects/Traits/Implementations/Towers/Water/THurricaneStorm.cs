using System.Collections.Generic;
using UnityEngine;

public class THurricaneStorm : Trait {
    public override TraitType Type => TraitType.HurricaneStorm;

    private Dictionary<
        /* target entity ID */ int,
        /* time of last paralysis */ float
    > TimeOfLastParalysisApplication { get; }

    private System.Random RNG;

    public THurricaneStorm(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        TimeOfLastParalysisApplication = new Dictionary<int, float>();
        RNG = new System.Random();

        tower.Attack.OnAttackLandedPost += MaybeApplyParalysis;
    }

    private void MaybeApplyParalysis(
        ServerEntity attacker,
        ServerEntity target
    ) {
        if (
            !target.AssociatedTraitTypes.Contains(TraitType.Flying)
            || (
                TimeOfLastParalysisApplication.TryGetValue(
                    target.ID,
                    out float lastParalysisApplicationTime
                )
                && Time.time - lastParalysisApplicationTime
                    < TraitConstants.HurricaneStormParalyzationImmunityDuration
                )
        ) {
            return;
        }

        if (RNG.NextDouble() > TraitConstants.HurricaneStormParalyzationProcChance) {
            return;
        }

        BuffFactory.ApplyBuff(
            BuffType.HurricaneStormParalysis,
            target,
            attacker
        );
        TimeOfLastParalysisApplication[target.ID] = Time.time;
    }

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
            TraitConstants.HurricaneStormMaxDamageDealtMultiplier
        );
    }
}