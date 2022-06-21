using System;
using UnityEngine;
using GeneralFunctions;

using CustomTiles;

public class Player : MonoBehaviour
{
    [SerializeField] private Int2 InitialPos = Int2.one;
    public void SetInitialPos(Int2 newInitialPos)
    {
        InitialPos = newInitialPos;
    }

    public static Action OnPlayerPosChange;


    private Int2 position;
    public Int2 Position
    {
        get
        {
            return position;
        }
        set
        {
            position = value;
            OnPlayerPosChange();
        }
    }

    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        position = InitialPos;
    }

    private void Start()
    {
        sprite.size = new Vector2(TileMap.GetTileSize(), TileMap.GetTileSize());
        InitialPos = TileMap.InitialTilePos;
    }


}
