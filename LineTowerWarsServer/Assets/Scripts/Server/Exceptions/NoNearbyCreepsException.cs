public class NoNearbyCreepsException : NotFoundException {
    public NoNearbyCreepsException() : base(
        $"Could not find any nearby creeps!"
    ) { }
}