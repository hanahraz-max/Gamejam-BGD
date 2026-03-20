using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;

public class TileShrinkManager : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase normalTile;
    public TileBase crack1Tile;
    public TileBase crack2Tile;
    public TileBase brokenTile;

    public int width = 36;
    public int height = 20;

    public float crackDelay = 1f;
    public float breakDelay = 0.4f;
    public float startDelay = 2f;

    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        width = bounds.size.x;
        height = bounds.size.y;
        BeginPressure();
    }

    Coroutine shrinkRoutine;

    public void BeginPressure()
    {
        if (shrinkRoutine == null)
        {
            shrinkRoutine = StartCoroutine(ShrinkRoutine());
        }
    }

    int GetLayer(int x, int y)
    {
        return Mathf.Min(
            x,
            y,
            width - 1 - x,
            height - 1 - y
        );
    }
    
    void SetLayerTile(int targetLayer, TileBase tile)
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                int localX = x - bounds.xMin;
                int localY = y - bounds.yMin;

                if (GetLayer(localX, localY) == targetLayer)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    tilemap.SetTile(pos, tile);
                }
            }
        }
    }

    IEnumerator ShrinkRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        int maxLayer = Mathf.Min(width, height) / 2;

        for (int layer = 0; layer < maxLayer; layer++)
        {
            SetLayerTile(layer, crack1Tile);
            yield return new WaitForSeconds(crackDelay);

            SetLayerTile(layer, crack2Tile);
            yield return new WaitForSeconds(crackDelay);

            SetLayerTile(layer, brokenTile);
            yield return new WaitForSeconds(breakDelay);
        }
    }
}

