using UnityEngine;

public class HoveringEntityStatusBar : MonoBehaviour
{
    private ClientEntity E { get; set; }

    [SerializeField] private ProgressBar StatusBar;

    private void Awake() {
        E = GetComponentInParent<ClientEntity>();
    }

    private void Start() {
        UpdateStatusBar(E);
        E.OnStatusUpdated += UpdateStatusBar;
    }

    private void UpdateStatusBar(ClientEntity e) {
        StatusBar.UpdateProgress(e.HP, e.MaxHP);
        StatusBar.UpdateColorByFillAmount();

        if (e.HP < e.MaxHP) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}
