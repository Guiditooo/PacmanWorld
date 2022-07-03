using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Transform tileMapPos;

    private void Awake()
    {
        tileMapPos = tileMap.transform;
        characterGroup = new List<Character>(prefabCharacterGroup.Length);
    }

    private void Start()
    {
        tileMap.Create(15, 15);
        foreach (Character character in prefabCharacterGroup)
        {
            Character newChar = null;
            if (character.tag == "Player")
            {
                newChar = Instantiate(character, new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * playerInitialPos.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * playerInitialPos.Y), Quaternion.identity);
                newChar.SetInitialPos(playerInitialPos);
                
            }
            else if (character.tag == "RandomStalker")
            {
                Int2 enemyPos = enemyInitialPos[Random.Range(0, enemyInitialPos.Length)];
                newChar = Instantiate(character, new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * enemyPos.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * enemyPos.Y), Quaternion.identity);
                newChar.SetInitialPos(TileMap.InitialStalkerPos);
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

    private void MoveCharacter(Character charToMove)
    {
        Vector3 targetPos = new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * charToMove.Position.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * charToMove.Position.Y);
        StartCoroutine(MovementLerper(charToMove.transform.position, targetPos, charToMove));
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
        //player.transform.position = auxPosition;
    }

    private void OnDestroy()
    {
        foreach (Character character in prefabCharacterGroup)
        {
            character.OnCharacterPosChange -= MoveCharacter;
        }
    }


}
