using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Power _power;

    public void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
    }

    private void OnEnable()
    {
        _power.ValueChanged += OnValueChanged;
        _slider.value = 0.01f;
    }

    private void OnDisable()
    {
        _power.ValueChanged -= OnValueChanged;
    }
}
