using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWaveSpawner : WaveSpawner
{
    public Queue<GameObject> Pool;

    private void Awake()
    {
<<<<<<< HEAD
        Pool = InitializePool(Pool);
    }

    public override void ReturnGameObject(GameObject gameObject, Queue<GameObject> pool)
    {
        base.ReturnGameObject(gameObject, Pool);
=======
        InitializePool(_pool);
>>>>>>> parent of b6142d2 (bugs bugs)
    }
}
