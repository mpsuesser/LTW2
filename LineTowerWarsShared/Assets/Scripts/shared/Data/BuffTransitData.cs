public class BuffTransitData {
    public int ID { get; }
    public BuffType Type { get; }
    public int Stacks { get; }
    public bool IsDurationBased { get; }
    public double FullDuration { get; }
    public double RemainingDuration { get; }
    
    public BuffTransitData(
        int buffID,
        BuffType buffType,
        int numStacks,
        bool isDurationBased,
        double fullDuration = 0,
        double remainingDuration = 0
    ) {
        ID = buffID;
        Type = buffType;
        Stacks = numStacks;
        IsDurationBased = isDurationBased;
        FullDuration = fullDuration;
        RemainingDuration = remainingDuration;
    }
}