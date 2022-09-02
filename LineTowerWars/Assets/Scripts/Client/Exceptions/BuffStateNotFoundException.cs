public class BuffStateNotFoundException : NotFoundException {
    public BuffStateNotFoundException(int buffID)
        : base($"A buff state for buff with ID {buffID} could not be found") {}
}