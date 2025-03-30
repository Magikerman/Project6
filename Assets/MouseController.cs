using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var FocusedTileHit = GetFocusedOnTile();

        if(FocusedTileHit.HasValue)
        {
            GameObject overlayTile = FocusedTileHit.Value.collider.gameObject;
            transform.position = overlayTile.transform.position + new Vector3(offsetX, offsetY, 0);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = overlayTile.GetComponent<SpriteRenderer>().sortingOrder+34;

            if (Input.GetMouseButtonDown(0))
            {
                overlayTile.GetComponent<OverlayTile>().ShowTile();
            }
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
}
