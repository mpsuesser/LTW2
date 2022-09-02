using UnityEngine;

public class GridCellOutline : MonoBehaviour
{
    public static GridCellOutline Create(Vector3 pos, Transform parent) {
        GridCellOutline outline = Instantiate(ClientPrefabs.Singleton.pfGridCellOutline, pos, Quaternion.identity, parent);

        return outline;
    }
}
