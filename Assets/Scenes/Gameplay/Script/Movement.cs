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
        //if (!IsMoving)
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

        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Me quiero mover a la derecha");
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
        Debug.Log("Me voy a mover a la derecha");
        if (player.Position.X + 1 < CustomTiles.TileMap.GetMapBounds().X)
        {
            player.Position = new Int2(player.Position.X + 1, player.Position.Y);
        }
    }
    public void MoveLeft()
    {
        if (player.Position.X - 1 >= 0)
        {
            Debug.Log("Me voy a mover a la izquierda");
            player.Position = new Int2(player.Position.X - 1, player.Position.Y);
        }

    }
    public void MoveUp()
    {
        Debug.Log("Me voy a mover para arriba");
        if (player.Position.Y + 1 < CustomTiles.TileMap.GetMapBounds().Y)
        {
            player.Position = new Int2(player.Position.X, player.Position.Y + 1);
        }
    }
    public void MoveDown()
    {
        Debug.Log("Me voy a mover para abajo");
        if (player.Position.Y - 1 >= 0)
        {
            player.Position = new Int2(player.Position.X, player.Position.Y - 1);
        }
    }

}