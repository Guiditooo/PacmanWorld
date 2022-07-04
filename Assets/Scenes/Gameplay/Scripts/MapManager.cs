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


    private void Awake()
    {
        characterGroup = new List<Character>(prefabCharacterGroup.Length);
    }

    private void Start()
    {
        tileMap.Create();

        foreach (Character character in prefabCharacterGroup)
        {
            Character newChar = null;
            if (character.tag == "Player")
            {
                newChar = Instantiate(character, TileConversor.GridToWorld(playerInitialPos), Quaternion.identity);
                newChar.SetInitialPos(playerInitialPos);
                
            }
            else if (character.tag == "RandomStalker")
            {
                Int2 enemyPos = enemyInitialPos[Random.Range(0, enemyInitialPos.Length)];
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
