public class ServerSwordsman : ServerEnemy
{
    public override EnemyType Type => EnemyType.Swordsman;
    
    protected override void Awake() {
        base.Awake();
        
        ProximityBuffApplicator_DevotionAura1.Create(this);
    }
}