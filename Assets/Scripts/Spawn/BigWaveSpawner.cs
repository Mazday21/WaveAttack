using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    private void Update()
    {
        if (_coroutineAllowed)
        {
            Debug.Log("big");
            StartCoroutine(DelayAppearance());
        }
    }
}
