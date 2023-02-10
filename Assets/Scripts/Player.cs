using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _minPower = 3;
    private int _maxPower = 20;

    public event UnityAction<int, int> PowerChanged;
    public event UnityAction<int> ScoreChanged;

    public int Power { get; private set; }
    public int Score { get; private set; }
    public int Level { get; private set; }
    
    private void Start()
    {
        Power = 3;
        Level = 1;
    }

    public void ChangePower(int power)
    {
        if (power < _minPower)
            Power = _minPower;
        else if(power > _maxPower)
            Power = _maxPower;
        else Power = power;
        PowerChanged?.Invoke(Power, _maxPower);
    }

    public void AddScore(int score)
    {
        Score += score;
        ScoreChanged?.Invoke(Score);
    }
}
