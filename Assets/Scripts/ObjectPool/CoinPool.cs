using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : ObjectPool
{
    [SerializeField] private RectTransform _destination;

    public RectTransform Destination { get; private set; }

    private void Awake()
    {
        Destination = _destination;
    }
}
