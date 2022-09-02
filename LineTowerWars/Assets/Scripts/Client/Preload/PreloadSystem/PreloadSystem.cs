using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadSystem : MonoBehaviour
{
    private void Start() {
        Invoke(nameof(OpenMainMenu), .5f);
    }

    private void OpenMainMenu() {
        SceneSystem.Singleton.LoadMainMenu();
    }
}
