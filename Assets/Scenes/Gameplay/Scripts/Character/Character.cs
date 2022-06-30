using System;
using UnityEngine;
using GeneralFunctions;

using CustomTiles;

public class Character : MonoBehaviour
{
    [SerializeField] private Int2 InitialPos = Int2.one;
    [SerializeField] private GameObject prefab;
    public GameObject Prefab 
    {
        get
        {
            return prefab;
        }
        private set
        {
            prefab = value;
        }
    }
    public CharacterType CharType { get; private set; }
    public void SetInitialPos(Int2 newInitialPos)
    {
        InitialPos = newInitialPos;
    }

    public Action<Character> OnCharacterPosChange;

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
            OnCharacterPosChange?.Invoke(this);
        }
    }

    private SpriteRenderer sprite;
    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sprite.size = new Vector2(TileMap.GetTileSize(), TileMap.GetTileSize());
        position = InitialPos;
    }
}
