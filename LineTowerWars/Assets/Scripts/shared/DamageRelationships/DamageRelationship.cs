using UnityEngine;

[CreateAssetMenu(fileName = "XAttackToYArmor", menuName = "ScriptableObjects/DamageRelationship", order = 1)]
public class DamageRelationship : ScriptableObject {
    [SerializeField] public AttackType SourceAttackType;
    [SerializeField] public ArmorType TargetArmorType;

    [Space(5)]
    [SerializeField] public double DamageModifier;
}