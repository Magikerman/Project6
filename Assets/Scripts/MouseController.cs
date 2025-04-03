using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int jumpDistance;
    [SerializeField] private int tileRange;
    public int JumpDistance {  get { return jumpDistance; } }
    
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private CharacterInfo character;
    public CharacterInfo Character { set { character = value; } }

    private PathFinder pathFinder;
    private RangeFinder rangeFinder;
    private List<OverlayTile> path = new List<OverlayTile>();
    private List<OverlayTile> inRangeTiles = new List<OverlayTile>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pathFinder = new PathFinder();
        rangeFinder = new RangeFinder();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var FocusedTileHit = GetFocusedOnTile();

        if(FocusedTileHit.HasValue)
        {
            OverlayTile overlayTile = FocusedTileHit.Value.collider.gameObject.GetComponent<OverlayTile>();
            transform.position = overlayTile.transform.position;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder+1;

            if (Input.GetMouseButtonDown(0))
            {
                path = pathFinder.FindPath(character._activeTile, overlayTile, inRangeTiles, jumpDistance);
            }
        }

        if (path.Count > 0)
        {
            MoveAlongPath();
        }
    }

    public void GetInRangeTiles()
    {
        foreach (var item in inRangeTiles)
        {
            item.HideTile();
        }

        inRangeTiles = rangeFinder.GetTilesInRange(character._activeTile, tileRange, jumpDistance);

        foreach (var item in inRangeTiles)
        {
            item.ShowTile();
        }
    }

    private void MoveAlongPath()
    {
        var step = speed * Time.deltaTime;

        var zIndex = path[0].transform.position.z;
        character.transform.position = Vector2.MoveTowards(character.transform.position, path[0].transform.position, step);
        character.transform.position = new Vector3(character.transform.position.x, character.transform.position.y, zIndex);

        if (Vector2.Distance(character.transform.position, path[0].transform.position) < 0.0001f)
        {
            PositionCharacterOnTile(path[0]);
            path.RemoveAt(0);
        }
        if (path.Count == 0)
        {
            GetInRangeTiles();
        }
    }
    
    public RaycastHit2D? GetFocusedOnTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2d, Vector2.zero);

        if(hit.Length > 0 )
        {
            return hit.OrderByDescending(i => i.collider.transform.position.z).First();
        }
        return null;
    }

    public void PositionCharacterOnTile(OverlayTile tile)
    {
        character.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.0001f, tile.transform.position.z);
        character.GetComponent<SpriteRenderer>().sortingOrder = tile.GetComponent<SpriteRenderer>().sortingOrder+2;
        character._activeTile = tile;
    }
}
