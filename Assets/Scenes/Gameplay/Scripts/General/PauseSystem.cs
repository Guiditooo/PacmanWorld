using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class PauseSystem : MonoBehaviour
{
    public static Action<PauseStates> OnPauseStateChange;
    public static bool Paused { get; private set; }
    private void Awake()
    {
        InputManager.OnPausePress += PauseControl;
    }

    private void Start()
    {
        Paused = false;
        PauseControl();
    }

    public static void PauseControl()
    {
        if (GameManager.GameRunning)
        {
            Paused = !Paused;
            Time.timeScale = Paused ? 0 : 1;
            PauseStates actualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
            Debug.Log("Envio estado de pausa: " + Paused);
            OnPauseStateChange?.Invoke(actualState);
        }
    }
    private void OnDestroy()
    {
        InputManager.OnPausePress -= PauseControl;
    }
}
