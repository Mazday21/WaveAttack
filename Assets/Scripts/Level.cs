using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private Power _power;
    private int _maxValue = 10;

    public int Value { get; private set; } = 1;

    private void OnPowerUp()
    {
        if(Value < _maxValue)
            Value += 1;
    }

    private void OnEnable()
    {
        _power.PowerUp += OnPowerUp;
    }

    private void OnDisable()
    {
        _power.PowerUp -= OnPowerUp;
    }
}
