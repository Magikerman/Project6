using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private OverlayTile activeTile;
    public OverlayTile _activeTile
    {
        get { return activeTile; }
        set { activeTile = value; }
    }

    [SerializeField] private bool atacking;
    public bool Atacking
    {
        get { return atacking; }
        set { atacking = value; }
    }

    [SerializeField] private int hp;
    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    [SerializeField] private int damage;
    public int Damage => damage;
}
