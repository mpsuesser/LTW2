using UnityEngine;

public class LTWLogger {
    public static void Log(string s) => Debug.Log("[LTW LOG]: " + s);
    public static void LogError(string s) => Debug.LogError("[LTW ERROR]: " + s);
    public static void LogDebug(string s) => Debug.Log("[LTW DEBUG]: " + s);
}
