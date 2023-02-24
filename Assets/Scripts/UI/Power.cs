using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    [SerializeField] private MinusFiveFalling _minusFive;
    [SerializeField] private DoubleFalling _double;
    [SerializeField] private TimeScale _timeScale;

    private int _minPower = 3;
    private int _maxPower = 20;
    private float _rotationTime = 10;
    private float _delayToResetValueTime = 0.55f;
    private float _timeScaleValue = 0.2f;

    public event UnityAction<int, int> ValueChanged;
    public event UnityAction PowerUp;

    public int Value { get; private set; }

    private void Start()
    {
        Value = _minPower;
    }

    public void ChangePower(PowerChanges change)
    {
        Value = change.PowerChange(Value);

        if(Value < _minPower)
        {
            Value = _minPower;
        }
        else if (Value > _maxPower)
        {
            Value = _maxPower;
            StartCoroutine(DelayResetValue());
        }
        ValueChanged?.Invoke(Value, _maxPower);
    }

    private IEnumerator DelayResetValue()
    {
        yield return new WaitForSeconds(_delayToResetValueTime);
        PowerUp?.Invoke();
        Value = _minPower;
        ValueChanged?.Invoke(Value, _maxPower);
        _timeScale.DilationTime(_timeScaleValue, _rotationTime);
    }
}
