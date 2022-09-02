using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class TargetFrame : MonoBehaviour {
    [SerializeField] private GameObject Content;

    [SerializeField] private SingleTargetInfoDisplay SingleTargetDisplay;
    [SerializeField] private MultipleTargetDisplay MultipleTargetDisplay;

    public void ClearAndHide() {
        Content.SetActive(false);
    }

    public void ShowForTargets(List<ClientEntity> targets) {
        if (targets.Count == 1) {
            MultipleTargetDisplay.Hide();
            SingleTargetDisplay.Load(targets.ToArray()[0]);
            SingleTargetDisplay.Show();
        } else {
            SingleTargetDisplay.Hide();
            MultipleTargetDisplay.Load(targets);
            MultipleTargetDisplay.Show();
        }

        Content.SetActive(true);
    }
}
