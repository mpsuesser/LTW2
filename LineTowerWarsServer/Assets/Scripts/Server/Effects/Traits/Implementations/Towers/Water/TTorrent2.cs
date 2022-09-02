using System.Collections.Generic;

public class TTorrent2 : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Torrent2;

    public TTorrent2(ServerEntity entity) : base(entity) {
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }

    public double GetInterval() =>
        TraitConstants.TorrentAura2StackApplicationIntervalInSeconds;
    public void DoPeriodicThing() {
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.TorrentAura2Radius,
                new CreepEntityFilter()
            );

        foreach (ServerEntity creep in creepsInRange) {
            BuffFactory.ApplyBuff(
                BuffType.TorrentSlow2,
                creep,
                E
            );
        }
    }
}