using UnityEngine;

public class EntityEventSystem : MonoBehaviour
{
    private void Start() {
        EventBus.OnEntityAttack += HandleEntityAttack;
    }

    private void OnDestroy() {
        EventBus.OnEntityAttack -= HandleEntityAttack;
    }
    
    private static void HandleEntityAttack(
        ClientEntity entity,
        AttackEventData eventData
    ) {
        entity.HandleAttackEvent(eventData);
    }
}
