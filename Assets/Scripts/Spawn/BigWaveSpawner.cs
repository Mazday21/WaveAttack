using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    public Queue<GameObject> Pool;

    private void Awake()
    {
        Pool = InitializePool(Pool);
    }

    public override void ReturnGameObject(GameObject gameObject, Queue<GameObject> pool)
    {
        base.ReturnGameObject(gameObject, Pool);
    }
}
