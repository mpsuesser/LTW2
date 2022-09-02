using UnityEngine;

[CreateAssetMenu(fileName = "TechImageResource", menuName = "ScriptableObjects/TechImageResource", order = 1)]
public class TechImageResource : ScriptableObject
{
    [Space(5)]
    [SerializeField] public ElementalTechType TechType;
    [SerializeField] public Sprite Image;
}