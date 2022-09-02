using System.Collections.Generic;
using UnityEngine;

public interface IHandleKeyUpInput : IHandleInput {
    HashSet<KeyCode> KeyUpSubscriptions { get; }
    bool HandleInputKeyUp(KeyCode kc);
}