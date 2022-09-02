using System;

public class NoSlotsRemainingException : Exception {
    public NoSlotsRemainingException(int maxSlots)
        : base($"All slots are already filled. Max slots = {maxSlots}") { }
}