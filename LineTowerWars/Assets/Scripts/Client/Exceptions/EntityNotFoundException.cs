public class EntityNotFoundException : NotFoundException {
    public EntityNotFoundException(int entityID)
        : base($"An entity with ID {entityID} could not be found") {}
}