public class ServerAcolyte : ServerEnemy
{
    public override EnemyType Type => EnemyType.Acolyte;
    
    protected override void Awake() {
        base.Awake();

        /* BuffFactory.ApplyBuff(
            BuffType.UnholySacrifice1,
            this,
            this
        ); */
    }
}