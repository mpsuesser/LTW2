public class BuffDescriptorNotFoundException : NotFoundException {
    public BuffDescriptorNotFoundException(BuffType buffType)
        : base($"A buff descriptor for buff of type {buffType} could not be found") {}
}