using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement),typeof(Character))]
public class PlayerControl : MonoBehaviour 
{
    private Movement movement;
    
    private void Awake()
    {
        movement = GetComponent<Movement>();
        InputManager.OnMovementPress += GetMovementTypeByInput;
        MapManager.OnCollisionWithOtherCharacter += StopMoving;
    }

    private void OnDestroy()
    {
        InputManager.OnMovementPress -= GetMovementTypeByInput;
        MapManager.OnCollisionWithOtherCharacter -= StopMoving;
    }

    private void GetMovementTypeByInput(MovementDirection movementDirection)
    {
        movement.SetNextMovement(movementDirection);
    }

    private void StopMoving()
    {
        movement.StopActualMovement();
    }

}
