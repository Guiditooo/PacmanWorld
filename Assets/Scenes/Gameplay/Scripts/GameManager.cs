using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static Action<int> OnScoreChange;

    public static Action<string> OnGameOver;

    public static Action OnLifeLost;

    private int lives;
    public static bool GameRunning { get; private set; }

    private int score;
    private void Awake()
    {
        GameRunning = true;
        PickUpAble.OnPickUp += AddPoints;
        PickUpAble.OnAllPickedUp += EndGame;
        MapManager.OnCollisionWithOtherCharacter += SubstractLife;
    }

    private void Start()
    {
        score = 0;
        lives = 3;
    }
    public void EndGame()
    {
        if (GameRunning)
        {
            if(PauseSystem.Paused)
            {
                PauseSystem.PauseControl();
            }
            GameRunning = false;
            Time.timeScale = 0;
            string endMsg = lives > 0 ? "Win" : "Lose";
            OnGameOver?.Invoke("You " + endMsg);
        }
    }

    private void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        OnScoreChange?.Invoke(score);
    }

    public void SubstractLife()
    {
        lives--;

        Debug.Log("Ahora tengo " + lives + " vidas.");

        OnLifeLost?.Invoke();

        if (lives == 0) EndGame();
    }

    private void OnDestroy()
    {
        PickUpAble.OnPickUp -= AddPoints;
        PickUpAble.OnAllPickedUp -= EndGame;
        MapManager.OnCollisionWithOtherCharacter -= SubstractLife;
    }
}
