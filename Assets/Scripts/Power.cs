using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Power : MonoBehaviour
{
    [SerializeField] private MinusFiveFalling _minusFive;
    [SerializeField] private DoubleFalling _double;

    private int _minPower = 3;
    private int _maxPower = 20;
    private int _changeValue = 2;
    private float _rotationTime = 2f;
    public event UnityAction<int, int> ValueChanged;
    public event UnityAction PowerUp;

    public int Value { get; private set; }

    private void Start()
    {
        Value = _maxPower;
    }

    public void ChangePower(PowerChanges change)
    {
        //if (change.name.StartsWith(_minusFive.name))
        //{
        //    Value -= _changeValue;

        //    if (Value < _minPower)
        //    {
        //        Value = _minPower;
        //    }

        //    ValueChanged?.Invoke(Value, _maxPower);
        //}
        //else
        //{
        //    Value *= _changeValue;

        //    if (Value > _maxPower)
        //    {
        //        Value = _maxPower;
        //        PowerUp?.Invoke();
        //        ValueChanged?.Invoke(Value, _maxPower);
        //        StartCoroutine(DelayResetValue());
        //    }
        //    ValueChanged?.Invoke(Value, _maxPower);
        //}
        StartCoroutine(DelayResetValue());
    }

    private IEnumerator DelayResetValue()
    {
        yield return new WaitForSeconds(0.5f);
        PowerUp?.Invoke();
        //Value = _minPower;
        ValueChanged?.Invoke(Value, _maxPower);
        Time.timeScale = 0.2f;
        StartCoroutine(DelayNormalizeTimaScale());
    }

    private IEnumerator DelayNormalizeTimaScale()
    {
        yield return new WaitForSeconds(_rotationTime);
        Time.timeScale = 1;
    }
}
