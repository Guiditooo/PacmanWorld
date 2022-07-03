using System;
using UnityEngine;
using GeneralFunctions;

using CustomTiles;

public class Character : MonoBehaviour
{
    [SerializeField] private Int2 InitialPos = Int2.one;
    [SerializeField] private GameObject prefab;
    [SerializeField] private CharacterType characterType;
    public GameObject Prefab 
    {
        get
        {
            return prefab;
        }
    }
    public CharacterType CharType 
    {
        get
        {
            return characterType;
        }
    }
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
            OnCharacterPosChange?.Invoke(this); //Deberia pasar su posicion, de modo que se pueda reubicar en la posicion global.
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
