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
    public bool IsMoving { set; get; }

    private Character character;

    public System.Action<MovementDirection> OnMove;

    private void Awake() 
    {
        character = GetComponent<Character>();
        movement = MoveRight;
        lastMovement = movement;
        IsMoving = false;

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

    public void SetNextMovement(MovementDirection movement)
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
    private void MoveRight()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.X++;
        if (character.Position.X + 1 < CustomTiles.TileMap.GetMapBounds().X && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            //Debug.Log("Me voy a mover a la derecha");
            character.Position = newcharacterPos;
            OnMove?.Invoke(MovementDirection.Right);
        }
        else
        {
            OnMove?.Invoke(MovementDirection.None);
        }
    }
    private void MoveLeft()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.X--;
        if (character.Position.X - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            //Debug.Log("Me voy a mover a la izquierda");
            character.Position = newcharacterPos;
            OnMove?.Invoke(MovementDirection.Left);
        }
        else
        {
            OnMove?.Invoke(MovementDirection.None);
        }
    }
    private void MoveUp()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.Y++;
        if (newcharacterPos.Y < CustomTiles.TileMap.GetMapBounds().Y && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            //Debug.Log("Me voy a mover para arriba");
            character.Position = newcharacterPos;
            OnMove?.Invoke(MovementDirection.Up);
        }
        else
        {
            OnMove?.Invoke(MovementDirection.None);
        }
    }
    private void MoveDown()
    {
        Int2 newcharacterPos = character.Position;
        newcharacterPos.Y--;
        if (character.Position.Y - 1 >= 0 && CustomTiles.TileMap.CheckForValidTile(newcharacterPos))
        {
            //Debug.Log("Me voy a mover para abajo");
            character.Position = newcharacterPos;
            OnMove?.Invoke(MovementDirection.Down);
        }
        else
        {
            OnMove?.Invoke(MovementDirection.None);
        }
    }

    public void StopActualMovement()
    {
        movement = null;
        nextMovement = null;
        lastMovement = null;
        OnMove?.Invoke(MovementDirection.None);
    }

}