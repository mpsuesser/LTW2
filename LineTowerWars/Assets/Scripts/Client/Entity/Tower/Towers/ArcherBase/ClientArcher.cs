using UnityEngine;

public class ClientArcher : ClientTower
{
    public override TowerType Type => TowerType.Archer;

    protected override void Awake() {
        PointInAttackAnimationOfAttack = 0.52f;

        base.Awake();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 31.5f);
    }
}
