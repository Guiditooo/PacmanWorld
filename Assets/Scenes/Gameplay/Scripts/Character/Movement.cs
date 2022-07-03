using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GeneralFunctions;

[RequireComponent(typeof(Character))]
public class Movement : MonoBehaviour
{
    private delegate void Move();
    private Move movement;
    private Move nextMovement;
    private Move lastMovement;

    private Character character;

    public bool IsMoving { set; get; }

    private void Awake()
    {
        character = gameObject.GetComponent<Character>();
        movement = MoveRight;
        lastMovement = movement;
        IsMoving = false;
        if (character.tag == "Player")
        {
            InputManager.OnMovementPress += SetNextMovement;
        }
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
    }

    private void SetNextMovement(MovementDirection movement)
    {
        switch (movement)
        {
            case MovementDirection.Down:
                nextMovement = MoveDown;
                break;
            case MovementDirection.Up:
                nextMovement = MoveUp;
                break;
            case MovementDirection.Left:
                nextMovement = MoveLeft;
                break;
            case MovementDirection.Right:
            default:
                nextMovement = MoveRight;
                break;
        }
    }
    public void MoveRight()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.X++;
        if (character.Position.X + 1 < CustomTiles.TileMap.GetMapBounds().X && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            Debug.Log("Me voy a mover a la derecha");
            character.Position = newcharacterPos;
        }
    }
    public void MoveLeft()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.X--;
        if (character.Position.X - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            Debug.Log("Me voy a mover a la izquierda");
            character.Position = newcharacterPos;
        }

    }
    public void MoveUp()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.Y++;
        if (character.Position.Y + 1 < CustomTiles.TileMap.GetMapBounds().Y && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            Debug.Log("Me voy a mover para arriba");
            character.Position = newcharacterPos;
        }
    }
    public void MoveDown()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.Y--;
        if (character.Position.Y - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            Debug.Log("Me voy a mover para abajo");
            character.Position = newcharacterPos;
        }
    }
    private void OnDestroy()
    {
        if (character.tag == "Player")
            InputManager.OnMovementPress -= SetNextMovement;
    }

}