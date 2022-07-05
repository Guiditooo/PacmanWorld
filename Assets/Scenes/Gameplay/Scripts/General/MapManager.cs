using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using GeneralFunctions;
using CustomTiles;


public class MapManager : MonoBehaviour //Se supone que funca una vez que se selecciono el nivel.
{
    [SerializeField] private CustomTiles.TileMap tileMap;
    [SerializeField] [Range(0.1f,4)]private float playerSpeed;

    [SerializeField] private Int2 playerInitialPos;
    [SerializeField] private Int2[] enemyInitialPos;


    [SerializeField] private Character[] prefabCharacterGroup;
    private List<Character> characterGroup;

    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject powerUpPrefab;

    [SerializeField] private Transform pointsFolder;
    [SerializeField] private Int2[] powerUpArray;
    private static PickUpAble[,] pickUpAbleArray;

    public static Action OnCollisionWithOtherCharacter;

    private void Awake()
    {
        characterGroup = new List<Character>(prefabCharacterGroup.Length);
    }

    private void Start()
    {
        tileMap.CreateTileMap();
        SetUpPickUpAbles();

        foreach (Character character in prefabCharacterGroup)
        {
            Character newChar = null;
            if (character.tag == "Player") //Reemplazar por algun patron de dienio.
            {
                newChar = Instantiate(character, TileConversor.GridToWorld(playerInitialPos), Quaternion.identity);
                newChar.SetInitialPos(playerInitialPos);
                
            }
            else if (character.tag == "RandomStalker")
            {
                Int2 enemyPos = enemyInitialPos[UnityEngine.Random.Range(0, enemyInitialPos.Length)];
                newChar = Instantiate(character, TileConversor.GridToWorld(enemyPos), Quaternion.identity);
                newChar.SetInitialPos(enemyPos);
            }
            else
            {
                Debug.Log("Characters Creation Error");
            }

            characterGroup.Add(newChar);

            if (newChar != null)
            {
                newChar.gameObject.name = character.name;
            }
        }
        for (int i = 0; i < characterGroup.Count; i++)
        {
            characterGroup[i].OnCharacterPosChange += MoveCharacter;
        }

    }
    private void SetUpPickUpAbles()
    {

        pickUpAbleArray = new PickUpAble[TileMap.Rows, TileMap.Columns];

        foreach (Int2 pos in powerUpArray)
        {
            GameObject otherGO;
            otherGO = Instantiate(powerUpPrefab, TileConversor.GridToWorld(pos.X, pos.Y), Quaternion.identity, pointsFolder);
            otherGO.name = "[ " + pos.X + " - " + pos.Y + " ] - " + powerUpPrefab.name;
            pickUpAbleArray[pos.X, pos.Y] = otherGO.GetComponent<PickUpAble>();
        }
        
        for (int y = 0; y < TileMap.Columns; y++)
        {
            for (int x = 0; x < TileMap.Rows; x++)
            {
                if (playerInitialPos.X == x && playerInitialPos.Y == y)
                {
                    continue;
                }
                if (TileMap.GetTileAtGridPos(x,y).Equals(TileType.Floor) && pickUpAbleArray[x, y] == null)
                {
                    GameObject otherGO;
                    otherGO = Instantiate(pointPrefab, TileConversor.GridToWorld(x, y), Quaternion.identity, pointsFolder);
                    otherGO.name = "[ " + x + " - " + y + " ] - " + pointPrefab.name;
                    pickUpAbleArray[x, y] = otherGO.GetComponent<PickUpAble>();
                }

            }
        }

    }


    private void MoveCharacter(Character charToMove)
    {
        StartCoroutine(MovementLerper(charToMove.transform.position, TileConversor.GridToWorld(charToMove.Position), charToMove));
    }
    private IEnumerator MovementLerper(Vector3 actualPos, Vector3 targetPos, Character charToMove)
    {
        float t = 0;
        charToMove.GetComponent<Movement>().IsMoving = true;
        while (t < 1)
        {
            t += Time.deltaTime * playerSpeed;
            if (t > 1) t = 1;
            charToMove.transform.position = Vector3.Lerp(actualPos, targetPos, t);
            yield return null;
        }
        charToMove.GetComponent<Movement>().IsMoving = false;

        //Esto es alta negrada pero es la forma de hacer algo cuando termina de moverse y no cuando recien empieza :P

        Character player = null;

        foreach (Character item in characterGroup)
        {
            if (item.gameObject.tag == "Player") player = item;
        }


        if (charToMove.tag == "Player")
        {
            if (pickUpAbleArray[charToMove.Position.X, charToMove.Position.Y] != null)
            {
                pickUpAbleArray[charToMove.Position.X, charToMove.Position.Y].PickUp();
            }
            foreach (Character item in characterGroup)
            {
                if (item.gameObject.tag == "Player") continue;
                if (item.Position == charToMove.Position)
                {
                    OnCollisionWithOtherCharacter?.Invoke();
                    charToMove.Position = playerInitialPos;
                }
            }

        }
        else
        {
            if (player.Position == charToMove.Position)
            {
                OnCollisionWithOtherCharacter?.Invoke();
                player.Position = playerInitialPos;
            }
        }

    }

    

    private void OnDestroy()
    {
        foreach (Character character in prefabCharacterGroup)
        {
            character.OnCharacterPosChange -= MoveCharacter;
        }
    }


}
