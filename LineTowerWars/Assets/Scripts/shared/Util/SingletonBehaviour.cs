using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour
{
    public static T Singleton { get; private set; }

    protected void InitializeSingleton(T entity) {
        Singleton = entity;
    }
}
