using System.Collections.Generic;
using UnityEngine;

public interface IHandleKeyDownInput : IHandleInput {
    HashSet<KeyCode> KeyDownSubscriptions { get; }
    bool HandleInputKeyDown(KeyCode kc);
}
