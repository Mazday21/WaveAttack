using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    private void Awake()
    {
        InitializePool(Pool);
    }
}
