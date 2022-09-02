using System.Collections.Generic;

public class TGermination1 : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Germination1;
    
    private int TicksSinceLastAttack { get; set; }

    public TGermination1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }
        
        TicksSinceLastAttack = 0;
        Ticker.Subscribe(this);

        tower.Attack.OnAttackFiredPre += ResetBuffApplicationInterval;
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() => TraitConstants.Germination1IdleStackIntervalInSeconds;
    public void DoPeriodicThing() {
        if (++TicksSinceLastAttack >= 2) {
            BuffFactory.ApplyBuff(
                BuffType.Germination1,
                E,
                E
            );
        }
    }

    private void ResetBuffApplicationInterval(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        Ticker.RefreshSubscription(this);
        TicksSinceLastAttack = 0;
    }
}