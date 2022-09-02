using UnityEngine;

public class TowerBaseAbilities : MonoBehaviour {
    [SerializeField] private GameObject towerOptions;
    [SerializeField] private GameObject attackerCreepOptions;
    [SerializeField] private GameObject builderOptions;

    private void Awake() {
        HideAll();
    }

    private void HideAll() {
        towerOptions.SetActive(false);
        attackerCreepOptions.SetActive(false);
        builderOptions.SetActive(false);
    }

    public void LoadForEntity(
        ClientEntity entity
    ) {
        HideAll();
        
        switch (entity) {
            case ClientTower _:
                towerOptions.SetActive(true);
                break;
            case ClientEnemy enemy:
                if (
                    TraitConstants.EnemyTraitMap[enemy.Type].Contains(
                        TraitType.Attacker
                    )
                ) {
                    attackerCreepOptions.SetActive(true);
                }

                break;
            case ClientBuilder _:
                builderOptions.SetActive(true);
                break;
        }
    }
}
