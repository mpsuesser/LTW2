using System;

public class BEssenceOfPower2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfPower2;
    
    private System.Random RNG { get; }

    public BEssenceOfPower2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        RNG = new Random();
        
        affectedEntity.OnDamageTaken += DamageTakenByAffectedEntity;
        OnRemoved += UnsubscribeFromOnDamageTaken;
    }

    private void UnsubscribeFromOnDamageTaken(Buff b) {
        if (AffectedEntity == null) {
            return;
        }

        AffectedEntity.OnDamageTaken -= DamageTakenByAffectedEntity;
    }

    private void DamageTakenByAffectedEntity(
        ServerEntity dealer,
        double amount,
        DamageType damageType,
        DamageSourceType sourceType
    ) {
        AffectedEntity.DealDamageTo(
            dealer,
            amount * TraitConstants.EssenceOfPower2DamageReturnMultiplier,
            DamageType.Spell,
            DamageSourceType.EssenceOfPower2StaticReturn
        );

        if (RNG.Next() < TraitConstants.EssenceOfPower2StunChance) {
            BuffFactory.ApplyBuff(
                BuffType.EssenceOfPowerStun,
                dealer,
                AffectedEntity
            );
        }
    }
}