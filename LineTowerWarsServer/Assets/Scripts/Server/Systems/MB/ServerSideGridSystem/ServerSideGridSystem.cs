using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSideGridSystem : SingletonBehaviour<ServerSideGridSystem>
{
    private Dictionary<ServerTower, MazeGridCell[]> CellsOccupiedByTower { get; set; }

    private void Awake() {
        InitializeSingleton(this);

        CellsOccupiedByTower = new Dictionary<ServerTower, MazeGridCell[]>();

        ServerEventBus.OnTowerDied += PutBlockageOnGridCells;
    }

    private void OnDestroy() {
        ServerEventBus.OnTowerDied -= PutBlockageOnGridCells;
    }

    public void SetCellsOccupiedByTower(MazeGridCell[] cells, ServerTower t) {
        foreach (MazeGridCell cell in cells) {
            cell.SetOccupied();
        }

        CellsOccupiedByTower.Add(t, cells);

        ServerSend.SetCellsOccupancy(t.ActiveLane, cells, true);
    }

    public void UpdateTowerReference(ServerTower oldRef, ServerTower newRef) {
        if (!CellsOccupiedByTower.ContainsKey(oldRef)) {
            return;
        }

        CellsOccupiedByTower[newRef] = CellsOccupiedByTower[oldRef];
        CellsOccupiedByTower.Remove(oldRef);
    }

    public void FreeCellsOccupiedByTower(ServerTower t) {
        if (!CellsOccupiedByTower.ContainsKey(t)) {
            return;
        }

        MazeGridCell[] cells = CellsOccupiedByTower[t];
        foreach (MazeGridCell cell in cells) {
            cell.UnsetOccupied();
        }

        ServerSend.SetCellsOccupancy(t.ActiveLane, cells, false);
    }

    private void PutBlockageOnGridCells(ServerTower killedTower) {
        FreeCellsOccupiedByTower(killedTower);
        
        LTWLogger.Log("TODO: Put down blockage!");
    }
}
