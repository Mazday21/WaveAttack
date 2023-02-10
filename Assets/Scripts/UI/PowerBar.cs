using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Player _player;

    public void OnValueChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
    }

    private void OnEnable()
    {
        _player.PowerChanged += OnValueChanged;
        _slider.value = _player.Power;
    }

    private void OnDisable()
    {
        _player.PowerChanged -= OnValueChanged;
    }
}
