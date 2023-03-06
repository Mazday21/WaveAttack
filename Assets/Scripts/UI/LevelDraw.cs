using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDraw : MonoBehaviour
{
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private Level _level;

    private void OnEnable()
    {
        _textCount.text = "Level " + _level.Value.ToString();

        Level.ValueChanged += OnLevelChanged;
    }

    private void OnDisable()
    {
        Level.ValueChanged -= OnLevelChanged;
    }

    private void OnLevelChanged()
    {
        _textCount.text = "Level " + _level.Value.ToString();
    }
}
