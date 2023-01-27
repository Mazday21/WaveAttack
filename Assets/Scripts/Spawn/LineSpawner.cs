using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSpawner : StickmanSpawner
{
    [SerializeField] private StickmanLine _linePrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private float[] _emptySpawnPozitionsZ;
    private bool _coroutineAllowed = true;

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

    private void Update()
    {
        if (_coroutineAllowed)
        {
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        _coroutineAllowed = false;
        WaitForSeconds waitForSeconds = new WaitForSeconds(3);
        Spawn();
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    public void AddEmptyPointZ(float positionZ)
    {
        for(int i = 0; i< _spawnPoints.Length; i++)
        {
            if(_emptySpawnPozitionsZ[i] == 0)
            {
                _emptySpawnPozitionsZ[i] = positionZ;
                break;
            }
        }
    }

    public void Spawn()
    {
        for(int i = 0;i < _emptySpawnPozitionsZ.Length; i++)
        {
            if (_emptySpawnPozitionsZ[i] != 0)
            {
                Vector3 spawnPosition = _spawnPoints[0].position;
                spawnPosition.z = _emptySpawnPozitionsZ[i];
                _emptySpawnPozitionsZ[i] = 0;
                GetOrInstantiateGameObject(out GameObject lineStickman);
                SetStickman(lineStickman, spawnPosition);
                break;
            }
        }
    }
}
