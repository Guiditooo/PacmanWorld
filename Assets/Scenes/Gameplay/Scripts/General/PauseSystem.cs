using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PauseStates
{
    Paused,
    Resumed
}

public class PauseSystem : MonoBehaviour
{
    public static Action<PauseStates> OnPauseStateChange;
    public static bool Paused { get; private set; } = false;
    private void Awake()
    {
        InputManager.PressingPause += PauseControl;
    }
    private void PauseControl()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0 : 1;
        PauseStates actualState = Paused ? PauseStates.Paused : PauseStates.Resumed;
        OnPauseStateChange?.Invoke(actualState);
    }
    private void OnDestroy()
    {
        InputManager.PressingPause -= PauseControl;
    }
}
