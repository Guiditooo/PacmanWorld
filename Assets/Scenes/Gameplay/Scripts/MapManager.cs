using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralFunctions;
using CustomTiles;
public class MapManager : MonoBehaviour //Se supone que funca una vez que se selecciono el nivel.
{
    [SerializeField] private CustomTiles.TileMap tileMap;
    [SerializeField] [Range(0.1f,4)]private float playerSpeed;

    [SerializeField] private List<Character> characterGroup = new List<Character>();
    private List<Movement> characterMovement = new List<Movement>();
    private Transform tileMapPos;
    private void Awake()
    {
        tileMapPos = tileMap.transform;
        foreach (Character character in characterGroup)
        {
            characterMovement.Add(character.gameObject.GetComponent<Movement>());
            character.OnCharacterPosChange += MoveCharacter;
        }
    }

    private void Start()
    {
        tileMap.Create(15, 15);
        foreach (Character character in characterGroup)
        {
            GameObject newGO = character.gameObject;
            if (character.tag == "Player")
            {
                newGO = Instantiate(character.Prefab, TileMap.InitialCharacterPos.ToVector3(), Quaternion.identity);
                character.SetInitialPos(TileMap.InitialCharacterPos);
            }
            else if (character.tag == "RandomStalker")
            {
                newGO = Instantiate(character.Prefab, TileMap.InitialStalkerPos.ToVector3(), Quaternion.identity);
                character.SetInitialPos(TileMap.InitialStalkerPos);
            }
            else
            {
                
                Debug.Log("Characters Creation Error");
            }
            newGO.name = character.name;
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
        Movement.IsMoving = true;
        Vector3 auxPosition = Vector3.zero;
        while (t < 1)
        {
            t += Time.deltaTime * playerSpeed;
            if (t > 1) t = 1;
            charToMove.transform.position = Vector3.Lerp(actualPos, targetPos, t);
            yield return null;
        }
        Movement.IsMoving = false;
        //player.transform.position = auxPosition;
    }

    private void OnDestroy()
    {
        foreach (Character character in characterGroup)
        {
            character.OnCharacterPosChange -= MoveCharacter;
        }
    }


}
