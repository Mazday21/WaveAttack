using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    private Queue<GameObject> _pool;

    private void Awake()
    {
        InitializePool(_pool);
    }
}
