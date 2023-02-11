using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction<int> ScoreChanged;

    public int Score { get; private set; }
    public int Level { get; private set; }
    
    private void Start()
    {
        Level = 1;
    }

    public void AddScore(int score)
    {
        Score += score;
        ScoreChanged?.Invoke(Score);
    }
}
