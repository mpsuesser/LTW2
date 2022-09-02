using RiptideNetworking;

public abstract class EventData<T> {
    public abstract void WriteDataToMessage(ref Message message);
    // Read data should be `= new T(ref message)`
}