using UnityEngine;

[CreateAssetMenu(fileName = "EnemyNameImageResource", menuName = "ScriptableObjects/EnemyImageResource", order = 1)]
public class EnemyImageResource : ScriptableObject
{
    [Space(5)]
    [SerializeField] public EnemyType Enemy;
    [SerializeField] public Sprite Image;
}