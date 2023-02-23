using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityAction<int> ScoreChangeCalled;
    public static event UnityAction ScoreChanged;

    public int Score { get; private set; }

    private void OnEnable()
    {
        ScoreChangeCalled += OnScoreChanged;
    }

    private void OnDisable()
    {
        ScoreChangeCalled -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        Score += value;

        if (Score < 0)
            Score = 0;

        ScoreChanged?.Invoke();
    }
}
