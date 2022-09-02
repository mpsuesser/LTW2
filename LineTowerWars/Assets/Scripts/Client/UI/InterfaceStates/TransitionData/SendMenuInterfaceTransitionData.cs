public class SendMenuInterfaceTransitionData : InterfaceTransitionData {
    public int TavernIndex { get; }

    public SendMenuInterfaceTransitionData(int tavernIndex) {
        TavernIndex = tavernIndex;
    }
}