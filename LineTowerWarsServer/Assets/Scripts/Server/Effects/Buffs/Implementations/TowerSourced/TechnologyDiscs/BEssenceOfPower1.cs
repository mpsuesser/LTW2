

using System;

public class BEssenceOfPower1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfPower1;
    
    private System.Random RNG { get; }

    public BEssenceOfPower1(
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
            amount * TraitConstants.EssenceOfPower1DamageReturnMultiplier,
            DamageType.Spell,
            DamageSourceType.EssenceOfPower1StaticReturn
        );

        if (RNG.Next() < TraitConstants.EssenceOfPower1StunChance) {
            BuffFactory.ApplyBuff(
                BuffType.EssenceOfPowerStun,
                dealer,
                AffectedEntity
            );
        }
    }
}