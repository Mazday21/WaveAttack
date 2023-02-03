using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    private Queue<GameObject> _pool;

    private void Awake()
    {
        _pool = InitializePool(_pool);
    }
}
