public class BTorrentSlow2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.TorrentSlow2;

    protected override int MaxStackCount =>
        TraitConstants.Torrent2MaxStacks;
    
    public BTorrentSlow2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 -
        TraitConstants.Torrent2MovementSpeedReductionPerStack * Stacks;
}