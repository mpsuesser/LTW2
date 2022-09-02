using System;

public class EmptyCollectionException : Exception {
    public EmptyCollectionException() : base(
        "The provided collection was empty"
    ) { }
}