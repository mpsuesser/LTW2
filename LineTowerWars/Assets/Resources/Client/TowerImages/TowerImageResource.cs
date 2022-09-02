using UnityEngine;

[CreateAssetMenu(fileName = "TowerNameImageResource", menuName = "ScriptableObjects/TowerImageResource", order = 1)]
public class TowerImageResource : ScriptableObject
{
    [Space(5)]
    [SerializeField] public TowerType Tower;
    [SerializeField] public Sprite Image;
}