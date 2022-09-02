public class ServerMudGolem : ServerEnemy
{
    public override EnemyType Type => EnemyType.MudGolem;
    
    protected override void Awake() {
        base.Awake();

        /* BuffFactory.ApplyBuff(
            BuffType.MudGolemResistance,
            this,
            this
        ); */
    }
}