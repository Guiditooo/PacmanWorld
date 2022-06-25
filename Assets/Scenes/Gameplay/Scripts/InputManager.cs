using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class InputManager : MonoBehaviour
{
    public static Action<MovementType> PressingMovement; 
    public static Action PressingPause;

    //Tengo que suscribir esto al evento de pausar, o simplemente lo pauso con el input?
    //Yo creo que lo lógico sería pausarlo con el evento; así no estoy mezclando funcionalidades.
    private void Update()
    {
        if (!PauseSystem.Paused)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) PressingMovement?.Invoke(MovementType.Down);

            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) PressingMovement?.Invoke(MovementType.Left);

            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) PressingMovement?.Invoke(MovementType.Right);

            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) PressingMovement?.Invoke(MovementType.Up);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            PressingPause?.Invoke();
        }
    }

    

}
