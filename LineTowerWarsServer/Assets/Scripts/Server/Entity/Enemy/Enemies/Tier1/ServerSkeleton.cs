public class ServerSkeleton : ServerEnemy
{
    public override EnemyType Type => EnemyType.Skeleton;

    protected override void Start() {
        base.Start();
        
        // Temp, just checking if it's first spawn event
        // Start should be called after the initial setup, which is important
        // since the initial setup is where we adjust health on death pact event
        /* if ((float) HP / MaxHealth > .95) {
            BuffFactory.ApplyBuff(
                BuffType.DeathPact1,
                this,
                this
            );
        } */
    }
}