using UnityEngine;

public static class BootStrapper {
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute() {
        LTWLogger.Log("Bootstrapping available if needed!");
    }

    private static void CreateObject<T>() where T : MonoBehaviour {
        if (Object.FindObjectOfType<T>() != null) return;

        GameObject obj = new GameObject(typeof(T).Name);
        obj.AddComponent<T>();
    }
}