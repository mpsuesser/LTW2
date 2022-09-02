using UnityEngine;

public class VisualEffect : MonoBehaviour {
    [SerializeField] private float duration;

    private void Start() {
        Destroy(gameObject, duration);
    }
}