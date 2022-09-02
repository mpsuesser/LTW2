public class NoNearbyTowersException : NotFoundException {
    public NoNearbyTowersException() : base(
        $"Could not find any nearby towers!"
    ) { }
}