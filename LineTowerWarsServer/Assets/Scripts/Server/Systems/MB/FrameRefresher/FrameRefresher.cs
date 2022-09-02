using UnityEngine;

// This is bad coupling -- would be better to find a clever way to split this out
// and achieve this from EventBus. The issue I'm running into is accomplishing this
// in the case where the frame-count-owning class is a non-MonoBehaviour attached to
// an object that is instantiated many times over (e.g. NavigationSystem on ServerEnemy)
public class FrameRefresher : MonoBehaviour {
    private void Update() {
        NavigationSystem.NumPathsCalculatedThisFrame = 0;
    }
}