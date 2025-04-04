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

    //poner en el gameManager
    public CharacterInfo player1;
    public CharacterInfo player2;

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

    void Start()
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
                    }
                }
            }
        }
    }

    public List<OverlayTile> GetNeighbourAtkTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, int jumpHeight, int range)
    {
        Dictionary<Vector2Int, OverlayTile> tileToSearch = new Dictionary<Vector2Int, OverlayTile>();

        if (searchTiles.Count > 0)
        {
            foreach (var tile in searchTiles)
            {
                tileToSearch.Add(tile.Grid2DPos, tile);
            }
        }
        else
        {
            tileToSearch = map;
        }

        List<OverlayTile> neighbours = new List<OverlayTile>();

        for (int i = 1; i < range+1; i++)
        {
            //arriba
            Vector2Int locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y + i);

            if (tileToSearch.ContainsKey(locationToCheck))
            {
                if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
                {
                    neighbours.Add(tileToSearch[locationToCheck]);
                }
            }

            //abajo
            locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y - i);

            if (tileToSearch.ContainsKey(locationToCheck))
            {
                if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
                {
                    neighbours.Add(tileToSearch[locationToCheck]);
                }
            }

            //derecha
            locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x + i, currentOverlayTile.GridPos.y);

            if (tileToSearch.ContainsKey(locationToCheck))
            {
                if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
                {
                    neighbours.Add(tileToSearch[locationToCheck]);
                }
            }

            //izquierda
            locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x - i, currentOverlayTile.GridPos.y);

            if (tileToSearch.ContainsKey(locationToCheck))
            {
                if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
                {
                    neighbours.Add(tileToSearch[locationToCheck]);
                }
            }
        }

        
        return neighbours; ;
    }

    public List<OverlayTile> GetNeighbourTiles(OverlayTile currentOverlayTile, List<OverlayTile> searchTiles, int jumpHeight)
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
            if(Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //abajo
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x, currentOverlayTile.GridPos.y - 1);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //derecha
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x + 1, currentOverlayTile.GridPos.y);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        //izquierda
        locationToCheck = new Vector2Int(currentOverlayTile.GridPos.x - 1, currentOverlayTile.GridPos.y);

        if (tileToSearch.ContainsKey(locationToCheck))
        {
            if (Mathf.Abs(currentOverlayTile.GridPos.z - tileToSearch[locationToCheck].GridPos.z) <= jumpHeight)
            {
                neighbours.Add(tileToSearch[locationToCheck]);
            }
        }

        return neighbours;
    }

    //mover a gameManager
    public void SendAtackTile(OverlayTile atackedTile, int damage)
    {
        if (atackedTile.Grid2DPos == player1._activeTile.Grid2DPos)
        {
            player1.Hp -= damage;
        }
        if (atackedTile.Grid2DPos == player2._activeTile.Grid2DPos)
        {
            player2.Hp -= damage;
        }
    }
}
