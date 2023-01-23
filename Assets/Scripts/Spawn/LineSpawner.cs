using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSpawner : StickmanSpawner
{
    [SerializeField] private StickmanLine _linePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private float[] _emptySpawnPozitionsZ;

    private void Start()
    {
        _emptySpawnPozitionsZ = new float[_spawnPoints.Length];
        Initialize(_linePrefab.gameObject);

        foreach(Transform i in _spawnPoints)
        {
            GetOrInstantiateGameObject(out GameObject lineStickman);
            SetStickman(lineStickman, i.position);
        }
    }

    public void AddEmptyPoint(float positionZ)
    {
        for(int i = 0; i< _spawnPoints.Length; i++)
        {
            if(_emptySpawnPozitionsZ[i] == 0)
            {
                _emptySpawnPozitionsZ[i] = positionZ;
            }
        }
    }

    public void Spawn()
    {
        foreach(Transform transform in _spawnPoints)
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (_emptySpawnPozitionsZ[i] != 0)
                {
                    transform.position.z = 
                }
            }
        }
    }
}
