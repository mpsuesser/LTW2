using System;

public class NoImplementationForProvidedEnumException : Exception {
    public NoImplementationForProvidedEnumException(int providedEnumValue) : base(
        $"An implementation has not yet been created for the provided enum value (raw val: {providedEnumValue})"
    ) { }
}