using System;

public class EntityIsNotAttackerException : Exception {
    public EntityIsNotAttackerException(ServerEntity entity) : base(
        $"Entity with ID {entity.ID} is not an attacker"
    ) { }
}