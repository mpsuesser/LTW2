using System.Collections.Generic;

public class TCripplingDecay : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.CripplingDecay;

    public TCripplingDecay(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_CripplingVulnerabilityAura.Create(entity);
        
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() =>
        TraitConstants.CripplingDecayAura2StackApplicationIntervalInSeconds;
    public void DoPeriodicThing() {
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.CripplingDecayAuraRadius,
                new CreepEntityFilter()
            );

        foreach (ServerEntity creep in creepsInRange) {
            BuffFactory.ApplyBuff(
                BuffType.CripplingDecaySlow,
                creep,
                E
            );
        }
    }
}