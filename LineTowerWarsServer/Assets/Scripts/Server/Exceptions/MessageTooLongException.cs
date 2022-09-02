using System;

public class MessageTooLongException : Exception {
    public MessageTooLongException(string given, int max) :
        base($"Message was too long! Max characters = {max}, given characters = {given.Length}") { }
}