using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralFunctions;
using CustomTiles;
public class MapManager : MonoBehaviour //Se supone que funca una vez que se selecciono el nivel.
{
    [SerializeField] private CustomTiles.TileMap tileMap;
    [SerializeField] [Range(0.1f,4)]private float playerSpeed;

    [SerializeField] private Character[] characterGroup;
    private Movement[] characterMovement;
    private Transform tileMapPos;
    private void Awake()
    {
        characterMovement = new Movement[characterGroup.Length];
        tileMapPos = tileMap.transform;
        for (int i = 0; i < characterGroup.Length; i++)
        {
            characterMovement[i] = characterGroup[i].gameObject.GetComponent<Movement>();
            characterGroup[i].OnCharacterPosChange += MoveCharacter;
        }
    }

    private void Start()
    {
        tileMap.Create(15, 15);
        foreach (Character character in characterGroup)
        {
            GameObject newGO = null;
            if (character.tag == "Player")
            {
                newGO = Instantiate(character.Prefab, TileMap.InitialCharacterPos.ToVector3(), Quaternion.identity);
                character.transform.position = TileMap.InitialCharacterPos.ToVector3();
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
            if (newGO != null)
            {
                newGO.name = character.name;
            }
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
        foreach (Character character in characterGroup)
        {
            character.OnCharacterPosChange -= MoveCharacter;
        }
    }


}
