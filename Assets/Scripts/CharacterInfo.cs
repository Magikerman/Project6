using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private OverlayTile activeTile;

    public OverlayTile _activeTile
    {
        get { return activeTile; }
        set { activeTile = value; }
    }

    [SerializeField] private int jumpHeight;
    public int JumpHeight { get { return jumpHeight; } }
}
