using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static Action<int> OnScoreChange;

    private int score;
    private void Awake()
    {
        PickUpAble.OnPickUp += AddPoints;
    }

    private void Start()
    {
        score = 0;
    }

    private void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
        OnScoreChange?.Invoke(score);
    }

    private void OnDestroy()
    {
        PickUpAble.OnPickUp -= AddPoints;
    }
}
