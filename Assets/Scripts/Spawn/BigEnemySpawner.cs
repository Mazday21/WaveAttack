using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemySpawner : EnemySpawner
{
    private int _SpawnLevel = 5;

    protected override void SpawnSet()
    {
        if(_level.Value >= _SpawnLevel)
            base.SpawnSet();
    }
}
