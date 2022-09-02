using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerEnemyEventSystem : SingletonBehaviour<ServerEnemyEventSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {

    }
}
