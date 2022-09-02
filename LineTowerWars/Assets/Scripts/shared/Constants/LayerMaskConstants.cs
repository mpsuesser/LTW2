public static class LayerMaskConstants {
    public static readonly int EnvironmentLayerMask = 1 << 6;
    public static readonly int LaneLayerMask = 1 << 7;
    public static readonly int NotWalkableLayerMask = 1 << 8;
    
    public static readonly int TowerLayerMask = 1 << 9;
    public static readonly int EnemyLayerMask = 1 << 10;
    public static readonly int BuilderLayerMask = 1 << 16;
    public static readonly int EntityLayerMask =
        TowerLayerMask | EnemyLayerMask | BuilderLayerMask;
}