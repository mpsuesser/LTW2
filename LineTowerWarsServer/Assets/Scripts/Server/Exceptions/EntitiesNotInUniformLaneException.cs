using System;

public class EntitiesNotInUniformLaneException : Exception {
    public EntitiesNotInUniformLaneException(int oneLaneID, int differentLaneID) : base(
        $"The set of entities provided had multiple lane IDs, two of which are: {oneLaneID}, {differentLaneID}"
    ) { }
}