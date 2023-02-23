using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreQueue : MonoBehaviour
{
    private Queue<int> _counter = new Queue<int>();

    public static UnityAction<int> ScoreChangeCalled;

    private void OnEnable()
    {
        ScoreChangeCalled += OnCounterChanged;
    }

    private void OnDisable()
    {
        ScoreChangeCalled -= OnCounterChanged;
    }

    private void OnCounterChanged(int score)
    {
        _counter.Enqueue(score);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Coin coin))
        {
            if(_counter.TryDequeue(out int result))
            {
                Player.ScoreChangeCalled(result);
            }
        }
    }
}
