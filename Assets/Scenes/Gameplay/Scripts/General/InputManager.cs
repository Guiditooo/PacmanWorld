using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static Action<MovementDirection> OnMovementPress;
    public static Action OnPausePress;

    private void Update()
    {
        if (!PauseSystem.Paused)
        {
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) OnMovementPress?.Invoke(MovementDirection.Down);

            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) OnMovementPress?.Invoke(MovementDirection.Left);

            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) OnMovementPress?.Invoke(MovementDirection.Right);

            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) OnMovementPress?.Invoke(MovementDirection.Up);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            OnPausePress?.Invoke();
        }
    }



}
