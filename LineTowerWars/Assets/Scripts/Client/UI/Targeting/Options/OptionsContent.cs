using UnityEngine;

public class OptionsContent : MonoBehaviour {
    [SerializeField] private TowerBaseAbilities baseAbilities;
    [SerializeField] private TowerUpgradeOptions upgradeOptions;

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void LoadAndShowForEntity(ClientEntity e) {
        baseAbilities.LoadForEntity(e);
        upgradeOptions.LoadForEntity(e);

        gameObject.SetActive(true);
    }
}
