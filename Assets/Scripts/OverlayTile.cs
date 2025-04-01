using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class OverlayTile : MonoBehaviour
{
    [SerializeField] private int g;
    [SerializeField] private int h;

    public int G { get { return g; } set { g = value; } }
    public int H { get { return h; } set { h = value; } }
    public int F { get {  return G + H; } }


    [SerializeField] private OverlayTile previousTile;
    public OverlayTile _previousTile { get { return previousTile; } set { previousTile = value; } }

    private Vector3Int gridPos;
    public Vector3Int GridPos { get { return gridPos; } set{ gridPos = value; } }

    private bool isBlocked;
    public bool IsBlocked { get { return isBlocked; } }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            HideTile();
        }
    }

    public void ShowTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    public void HideTile()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
}
