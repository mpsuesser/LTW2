using UnityEngine;

[CreateAssetMenu(fileName = "BuffDescriptor", menuName = "ScriptableObjects/BuffDescriptor", order = 1)]
public class BuffDescriptor : ScriptableObject {
    [SerializeField] public BuffType Type;

    [SerializeField] public Sprite Image;
}