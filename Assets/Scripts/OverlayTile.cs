using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class OverlayTile : MonoBehaviour
{
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
