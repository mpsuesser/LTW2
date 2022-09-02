using System;
using System.Collections.Generic;
using UnityEngine;

public class DamageRelationships : SingletonBehaviour<DamageRelationships> {
    private static readonly HashSet<(AttackType, ArmorType, float)> Relationships =
        new HashSet<(AttackType, ArmorType, float)>() {
            (AttackType.Flux, ArmorType.Light, 1.25f),
            (AttackType.Flux, ArmorType.Medium, .80f),
            (AttackType.Flux, ArmorType.Heavy, 1.50f),
            (AttackType.Flux, ArmorType.Fortified, .66f),
            (AttackType.Flux, ArmorType.Hero, .66f),
            (AttackType.Flux, ArmorType.Unarmored, 1.00f),
            (AttackType.Chaos, ArmorType.Light, 1.00f),
            (AttackType.Chaos, ArmorType.Medium, 1.00f),
            (AttackType.Chaos, ArmorType.Heavy, 1.00f),
            (AttackType.Chaos, ArmorType.Fortified, 1.00f),
            (AttackType.Chaos, ArmorType.Hero, 1.00f),
            (AttackType.Chaos, ArmorType.Unarmored, 1.00f),
            (AttackType.Normal, ArmorType.Light, .80f),
            (AttackType.Normal, ArmorType.Medium, 1.50f),
            (AttackType.Normal, ArmorType.Heavy, 1.00f),
            (AttackType.Normal, ArmorType.Fortified, .80f),
            (AttackType.Normal, ArmorType.Hero, .80f),
            (AttackType.Normal, ArmorType.Unarmored, 1.00f),
            (AttackType.Piercing, ArmorType.Light, 1.50f),
            (AttackType.Piercing, ArmorType.Medium, 1.00f),
            (AttackType.Piercing, ArmorType.Heavy, .80f),
            (AttackType.Piercing, ArmorType.Fortified, .66f),
            (AttackType.Piercing, ArmorType.Hero, .66f),
            (AttackType.Piercing, ArmorType.Unarmored, 1.50f),
            (AttackType.Siege, ArmorType.Light, 1.00f),
            (AttackType.Siege, ArmorType.Medium, .80f),
            (AttackType.Siege, ArmorType.Heavy, .80f),
            (AttackType.Siege, ArmorType.Fortified, 1.50f),
            (AttackType.Siege, ArmorType.Hero, .80f),
            (AttackType.Siege, ArmorType.Unarmored, 1.20f),
        };
    
    private Dictionary<AttackType, Dictionary<ArmorType, float>> AttackToArmorRelationships { get; set; }
    private Dictionary<ArmorType, Dictionary<AttackType, float>> ArmorToAttackRelationships { get; set; }

    private void Awake() {
        InitializeSingleton(this);

        InitDicts();
    }

    private void InitDicts() {
        AttackToArmorRelationships = new Dictionary<AttackType, Dictionary<ArmorType, float>>();
        ArmorToAttackRelationships = new Dictionary<ArmorType, Dictionary<AttackType, float>>();
        
        foreach (AttackType attackType in Enum.GetValues(typeof(AttackType))) {
            AttackToArmorRelationships[attackType] = new Dictionary<ArmorType, float>();
        }
        foreach (ArmorType armorType in Enum.GetValues(typeof(ArmorType))) {
            ArmorToAttackRelationships[armorType] = new Dictionary<AttackType, float>();
        }
        
        foreach (
            (AttackType attackType, ArmorType armorType, float modifier)
            in Relationships
        ) {
            AttackToArmorRelationships[attackType][armorType] = modifier;
            ArmorToAttackRelationships[armorType][attackType] = modifier;
        }
    }

    public double GetModifierForRelationship(AttackType attackType, ArmorType armorType) {
        if (
            AttackToArmorRelationships[attackType].TryGetValue(
                armorType,
                out float modifier
            )
        ) {
            return modifier;
        }

        return 1.0;
    }
}
