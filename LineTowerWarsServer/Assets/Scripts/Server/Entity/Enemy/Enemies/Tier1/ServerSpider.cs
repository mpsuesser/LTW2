public class ServerSpider : ServerEnemy
{
    public override EnemyType Type => EnemyType.Spider;

    protected override void Awake() {
        base.Awake();
    }
}