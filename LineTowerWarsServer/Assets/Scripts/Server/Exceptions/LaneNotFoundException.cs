public class LaneNotFoundException : NotFoundException {
    public LaneNotFoundException(int laneID) : base(
        $"Could not find lane with ID {laneID}"
    ) { }

    public LaneNotFoundException() : base(
        "Could not find a lane"
    ) { }
}