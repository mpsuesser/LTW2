public static class UniqueID {
    private static int _entityCount = 0;
    public static int NextEntityID => ++_entityCount;

    private static int _buffCount = 0;
    public static int NextBuffID => ++_buffCount;
}