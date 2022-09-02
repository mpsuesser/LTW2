public class BuffEffectNotFoundException : NotFoundException {
    public BuffEffectNotFoundException(BuffType buffType)
        : base($"A buff effect for buff of type {buffType} could not be found") {}
}