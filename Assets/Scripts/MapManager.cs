using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MapManager : MonoBehaviour
{
    private static MapManager _instance;
    public static MapManager Instance { get { return _instance; } }

    public OverlayTile overlayTilePrefab;
    public GameObject overlayContainer;

    public Dictionary <Vector2Int, OverlayTile> map;

    private int numberOfTiles = 0;
    
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }else
        {
            _instance = this;
        }
    }

    void OnEnable()
    {
        var tileMap = gameObject.GetComponentInChildren<Tilemap>();

        map = new Dictionary<Vector2Int, OverlayTile>();

        BoundsInt bounds = tileMap.cellBounds;

        //todas las tiles
        for (int z = bounds.max.z; z >= bounds.min.z; z--)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                for (int x = bounds.min.x; x < bounds.max.x; x++)
                {
                    var tileLocation = new Vector3Int(x, y, z);
                    var tileKey = new Vector2Int(x, y);

                    if (tileMap.HasTile(tileLocation) && !map.ContainsKey(tileKey))
                    {
                        var overlayTile = Instantiate(overlayTilePrefab, overlayContainer.transform);
                        var cellWorldPosition = tileMap.GetCellCenterWorld(tileLocation);

                        overlayTile.transform.position = new Vector3(cellWorldPosition.x, cellWorldPosition.y, cellWorldPosition.z+1);
                        overlayTile.GetComponent<SpriteRenderer>().sortingOrder = tileMap.GetComponent<TilemapRenderer>().sortingOrder +1;
                        overlayTile.GridPos = tileLocation;
                        map.Add(tileKey, overlayTile);

                        overlayTile.name = "InvisTile " + numberOfTiles.ToString();
                        numberOfTiles++;
                    }
                }
            }
        }
    }

    public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, int jumpDistance)
    {
        Dictionary<Vector2Int, OverlayTile> tileToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if (searchTiles.Count > 0)
        {
            foreach (var tile in searchTiles)
            {
                tileToSearch.Add(tile.Grid2DPos, tile);
            }
        }else
        {
            tileToSearch = map;
        }


        List<OverlayTile> neighbours = new List<OverlayTile>();

        //arriba
        Vector2Int locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y + 1);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if(Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpDistance)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //abajo
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y - 1);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpDistance)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //derecha
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x + 1, currentOverlayTile.GridPos.y);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpDistance)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //izquierda
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x - 1, currentOverlayTile.GridPos.y);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpDistance)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        return neighbours;
    }
    public OverlayTile SearchForTile(Vector2Int gridPos)
    {
        for (int i = 0; i > numberOfTiles; i++)
        {
            GameObject tile = GameObject.Find("InvisTile " + i.ToString());

            if (tile.GetComponent<OverlayTile>().Grid2DPos == gridPos)
            {
                return tile.GetComponent<OverlayTile>();
            }
        }
        return null;
    }
}
