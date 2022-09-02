public class BTorrentSlow1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.TorrentSlow1;

    protected override int MaxStackCount =>
        TraitConstants.Torrent1MaxStacks;
    
    public BTorrentSlow1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        1 -
        TraitConstants.Torrent1MovementSpeedReductionPerStack * Stacks;
}