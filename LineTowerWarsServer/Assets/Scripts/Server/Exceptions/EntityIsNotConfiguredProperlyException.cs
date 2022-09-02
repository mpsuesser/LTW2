using System;

public class EntityIsNotConfiguredProperlyException : Exception {
    public EntityIsNotConfiguredProperlyException(ServerEntity entity) : base(
        $"Entity with ID {entity.ID} is not configured properly"
    ) { }
}