using System.Collections.Generic;

public class TTorrent1 : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Torrent1;

    public TTorrent1(ServerEntity entity) : base(entity) {
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() =>
        TraitConstants.TorrentAura1StackApplicationIntervalInSeconds;
    public void DoPeriodicThing() {
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.TorrentAura1Radius,
                new CreepEntityFilter()
            );

        foreach (ServerEntity creep in creepsInRange) {
            BuffFactory.ApplyBuff(
                BuffType.TorrentSlow1,
                creep,
                E
            );
        }
    }
}