using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private OverlayTile activeTile;
    [SerializeField] private MouseController cursor;

    [SerializeField] private Vector2Int startPos;
    [SerializeField] private OverlayTile startTile;

    public OverlayTile _activeTile
    {
        get { return activeTile; }
        set { activeTile = value; }
    }

    private void Start()
    {
        cursor.Character = this.GetComponent<CharacterInfo>();
        startTile = MapManager.Instance.SearchForTile(startPos);

        if (startTile == null )
        {
            startTile = GameObject.Find("InvisTile 38").GetComponent<OverlayTile>();
        }

        cursor.PositionCharacterOnTile(startTile);
        cursor.GetInRangeTiles();
    }
}
