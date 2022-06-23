using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralFunctions;

[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour
{
    private Int2 bounds = new Int2(0,0);

    private delegate void Move();
    private Move movement;
    private Move nextMovement;
    private Move lastMovement;

    private Player player;

    public static bool IsMoving { set; get; }

    private void Awake()
    {
        player = GetComponent<Player>();
        movement = MoveRight;
        lastMovement = movement;
        IsMoving = false;
    }
    public void SetBounds(Int2 newBounds) 
    { 
        bounds = new Int2(newBounds.X,newBounds.Y); 
    }
    
    private void LateUpdate()
    {
        if (!IsMoving)
            movement?.Invoke();
    }
    private void Update()
    {
        if (!IsMoving)
        {
            movement -= lastMovement;
            movement += nextMovement;
            lastMovement = movement;
        }
        else
        {
            if(nextMovement != movement)
            {
                movement = nextMovement;
                lastMovement = movement;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            nextMovement = MoveRight;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            nextMovement = MoveLeft;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            nextMovement = MoveUp;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            nextMovement = MoveDown;
        }
    }

    public void MoveRight()
    {
        Int2 newPlayerPos = player.Position;
        newPlayerPos.X++;
        if (player.Position.X + 1 < CustomTiles.TileMap.GetMapBounds().X && CustomTiles.TileMap.CheckForValidTile(newPlayerPos))
        {
            player.Position = new Int2(player.Position.X + 1, player.Position.Y);
        }
    }
    public void MoveLeft()
    {
        Int2 newPlayerPos = player.Position;
        newPlayerPos.X--;
        if (player.Position.X - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newPlayerPos))
        {
            Debug.Log("Me voy a mover a la izquierda");
            player.Position = new Int2(player.Position.X - 1, player.Position.Y);
        }

    }
    public void MoveUp()
    {
        Int2 newPlayerPos = player.Position;
        newPlayerPos.Y++;
        if (player.Position.Y + 1 < CustomTiles.TileMap.GetMapBounds().Y && CustomTiles.TileMap.CheckForValidTile(newPlayerPos))
        {
            player.Position = new Int2(player.Position.X, player.Position.Y + 1);
        }
    }
    public void MoveDown()
    {
        Int2 newPlayerPos = player.Position;
        newPlayerPos.Y--;
        if (player.Position.Y - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newPlayerPos))
        {
            player.Position = new Int2(player.Position.X, player.Position.Y - 1);
        }
    }

}