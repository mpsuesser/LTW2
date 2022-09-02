using System;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "") : base(message) { }
}

public class ResourceNotFoundException : NotFoundException
{
    public ResourceNotFoundException(string message = "") : base(message) { }
}
