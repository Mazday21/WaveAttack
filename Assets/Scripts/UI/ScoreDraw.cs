using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDraw : MonoBehaviour
{
    [SerializeField] private TMP_Text _textCount;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _textCount.text = _player.Score.ToString();

        Player.ScoreChanged += OnScoreChanged;
    }

    private void OnDisable()
    {
        Player.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged()
    {
        _textCount.text = _player.Score.ToString();
    }
}
