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

        InputManager.OnMovementPress += (movementType) =>
        {
            movement.SetNextMovement(movementType);
        };


    }
}
