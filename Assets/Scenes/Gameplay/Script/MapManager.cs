using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using GeneralFunctions;
using CustomTiles;

[CustomEditor(typeof(MapManager))]
public class MapManager : MonoBehaviour //Se supone que funca una vez que se selecciono el nivel.
{
    [SerializeField] private CustomTiles.TileMap tileMap;
    [Space(15)]
    [SerializeField] private GameObject playerPrefab;
    [Space(5)]
    [SerializeField] [Range(0.1f,4)]private float playerSpeed;

    private Player player;
    private Movement playerMovement;
    private Transform tileMapPos;
    private void Awake()
    {
        playerMovement = playerPrefab.GetComponent<Movement>();
        tileMapPos = tileMap.transform;
        Player.OnPlayerPosChange += MovePlayer;
    }

    private void Start()
    {
        tileMap.Create(15, 15);
        GameObject newGO = Instantiate(playerPrefab, TileMap.InitialTilePos, Quaternion.identity);
        newGO.name = "Player";
        player = newGO.GetComponent<Player>();
        playerMovement.SetBounds(TileMap.GetMapBounds());
    }

    private void Update()
    {
        
    }

    private void MovePlayer()
    {
        Vector3 targetPos = new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * player.Position.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * player.Position.Y);
        StartCoroutine(MovementLerper(player.transform.position, targetPos));
    }
    private IEnumerator MovementLerper(Vector3 actualPos, Vector3 targetPos)
    {
        float t = 0;
        Movement.IsMoving = true;
        Vector3 auxPosition = Vector3.zero;
        while (t < 1)
        {
            t += Time.deltaTime * playerSpeed;
            if (t > 1) t = 1;
            player.transform.position = Vector3.Lerp(actualPos, targetPos, t);
            yield return null;
        }
        Movement.IsMoving = false;
        //player.transform.position = auxPosition;
    }

    private void OnDestroy()
    {
        Player.OnPlayerPosChange -= MovePlayer;
    }


}
