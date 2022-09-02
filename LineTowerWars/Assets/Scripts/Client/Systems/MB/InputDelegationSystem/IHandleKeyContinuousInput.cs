using System.Collections.Generic;
using UnityEngine;

public interface IHandleKeyContinuousInput : IHandleInput {
    HashSet<KeyCode> KeyContinuousSubscriptions { get; }
    bool HandleInputKeyContinuous(KeyCode kc);
}