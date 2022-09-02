using System;

public class EntityIsNotCommandableException : Exception {
    public EntityIsNotCommandableException(ServerEntity entity) : base(
        $"Entity with ID {entity.ID} is not commandable"
    ) { }
}