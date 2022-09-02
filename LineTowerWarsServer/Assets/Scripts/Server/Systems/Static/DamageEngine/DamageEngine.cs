using UnityEngine;

public static class DamageEngine {
    public static void AdjustOutboundDamage(
        ServerEntity damageDealer,
        ServerEntity damageReceiver,
        ref double damage,
        DamageType damageType,
        DamageSourceType sourceType
    ) {
        if (damageType == DamageType.Pure) {
            return;
        }
        
        foreach (Effect.BaseDamageDealtAdjustment damageAdjustment
            in damageDealer.Effects.AggregatePreMultiplierDamageDealtAdjustments
        ) {
            damageAdjustment(damageReceiver, ref damage);
        }
        
        damage *= damageDealer.Effects.AggregateDamageDealtMultiplier;
    }

    public static void AdjustInboundDamage(
        ServerEntity damageReceiver,
        ServerEntity damageDealer,
        ref double damage,
        DamageType damageType,
        DamageSourceType sourceType
    ) {
        switch (damageType) {
            case DamageType.Pure:
                return;
            case DamageType.Physical: {
                AdjustForAttackArmorRelationship(
                    damageDealer,
                    damageReceiver,
                    ref damage
                );

                if (!damageDealer.Effects.AggregateAttacksIgnoreArmorValue) {
                    AdjustForReceiverArmor(
                        damageReceiver,
                        ref damage
                    );
                }

                if (sourceType == DamageSourceType.AutoAttackSplash) {
                    AdjustForReceiverPhysicalSplashDamageTaken(
                        damageReceiver,
                        ref damage
                    );
                }

                break;
            }
            case DamageType.Spell:
                AdjustForReceiverSpellResist(
                    damageReceiver,
                    ref damage
                );
                break;
        }

        damage *= damageReceiver.Effects.AggregateDamageTakenMultiplier;

        foreach (Effect.PostDamageTakenAdjustment damageAdjustment
            in damageReceiver.Effects.AggregatePostMultiplierDamageTakenAdjustments
        ) {
            damageAdjustment(damageDealer, ref damage);
        }
    }

    private static void AdjustForAttackArmorRelationship(
        ServerEntity attacker,
        ServerEntity receiver,
        ref double damage
    ) {
        damage *= DamageRelationships.Singleton.GetModifierForRelationship(
            attacker.AttackModifier,
            receiver.ArmorModifier
        );
    }

    private static void AdjustForReceiverArmor(
        ServerEntity receiver,
        ref double damage
    ) {
        float totalArmor =
            Mathf.Max(
                receiver.Armor - receiver.Effects.AggregateArmorReductionNotBelowZero,
                0
            )
            + receiver.Effects.AggregateArmorDiff;
        float damageTakenMultiplier = CalculateDamageTakenMultiplierFromArmorAmount(
            totalArmor
        );
        damage *= damageTakenMultiplier;
    }
    
    private static void AdjustForReceiverSpellResist(
        ServerEntity receiver,
        ref double damage
    ) {
        float totalSpellResist = receiver.SpellResist + receiver.Effects.AggregateSpellResistDiff;
        float damageTakenMultiplier = CalculateDamageTakenMultiplierFromSpellResistAmount(
            totalSpellResist
        );
        damage *= damageTakenMultiplier;
    }
    
    private static void AdjustForReceiverPhysicalSplashDamageTaken(
        ServerEntity receiver,
        ref double damage
    ) {
        damage *= receiver.Effects.AggregatePhysicalSplashDamageTakenMultiplier;
    }

    private static float CalculateDamageTakenMultiplierFromArmorAmount(
        float armor
    ) {
        return 1 - (0.06f * armor 
               / (1 + 0.06f * armor));
    }

    private static float CalculateDamageTakenMultiplierFromSpellResistAmount(
        float spellResist
    ) {
        return 1 - (0.06f * spellResist
               / (1 + 0.06f * spellResist));
    }
}