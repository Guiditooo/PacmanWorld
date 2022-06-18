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
        player.transform.position = new Vector3(TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * player.Position.X, TileMap.GetTileSize() / 2 + TileMap.GetTileSize() * player.Position.Y);
    }

    private void OnDestroy()
    {
        Player.OnPlayerPosChange -= MovePlayer;
    }

}
