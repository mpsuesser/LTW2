using System;

public class ClientDoesNotControlLaneException : Exception {
    public ClientDoesNotControlLaneException(int clientID, int laneID) : base(
        $"Client with ID {clientID} does not control lane {laneID}"
    ) { }
}