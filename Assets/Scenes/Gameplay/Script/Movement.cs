using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralFunctions;

[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour
{
    private Int2 bounds = Int2.zero;

    private delegate void Move();
    private Move movement;
    private Move lastMovement;

    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
        movement = MoveRight;
        lastMovement = movement;
    }
    public void SetBounds(Int2 newBounds) 
    { 
        bounds = newBounds; 
    }
    
    private void LateUpdate()
    {
        movement();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("Me quiero mover a la derecha");
            movement -= lastMovement;
            movement += MoveRight;
            lastMovement = MoveRight;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            movement -= lastMovement;
            movement += MoveLeft;
            lastMovement = MoveLeft;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            movement -= lastMovement;
            movement += MoveUp;
            lastMovement = MoveUp;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movement -= lastMovement;
            movement += MoveDown;
            lastMovement = MoveDown;
        }
    }

    public void MoveRight()
    {
        Debug.Log("Me voy a mover a la derecha");
        if (player.Position.X + 1 < bounds.X)
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
        if (player.Position.Y + 1 < bounds.Y)
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