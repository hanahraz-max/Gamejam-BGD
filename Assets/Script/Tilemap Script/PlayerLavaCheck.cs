using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerLavaCheck : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase brokenTile;

    void Update()
    {
        Vector3 worldPos = transform.position;

        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        TileBase currentTile = tilemap.GetTile(cellPos);

        if (currentTile == brokenTile)
        {
            Debug.Log("PLAYER MATI (LAVA)");
        }
    }
}