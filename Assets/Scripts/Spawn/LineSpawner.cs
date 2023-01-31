using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSpawner : StickmanSpawner
{
    [SerializeField] private StickmanLine _linePrefab;
    [SerializeField] private Transform[] _points;
    [SerializeField] private WeaponSpawner _weaponSpawner;

    private bool _coroutineAllowed = true;
    private SpawnPoint[] _spawnPoints;

    private void Start()
    {
        Initialize(_linePrefab.gameObject);

        foreach(Transform i in _points)
        {
            GetOrInstantiateGameObject(out GameObject lineStickman);
            SetStickman(lineStickman, i.position);
        }
        _spawnPoints = new SpawnPoint[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            _spawnPoints[i] = new SpawnPoint(_points[i], false);
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
        WaitForSeconds waitForSeconds = new WaitForSeconds(10);
        Spawn();
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    public void AddEmptyPointZ(float positionZ)
    {
        for(int i = 0; i< _spawnPoints.Length; i++)
        {
            if(!_spawnPoints[i].IsEnable)
            {
                _spawnPoints[i].IsEnable = true;
                _weaponSpawner.ChangeConditionSpawnPoint(positionZ);
                break;
            }
        }
    }

    public void Spawn()
    {
        for(int i = 0;i < _spawnPoints.Length; i++)
        {
            if (_spawnPoints[i].IsEnable)
            {
                _weaponSpawner.ChangeConditionSpawnPoint(_spawnPoints[i].Transform.position.z);
                GetOrInstantiateGameObject(out GameObject lineStickman);
                SetStickman(lineStickman, _spawnPoints[i].Transform.position);
                break;
            }
        }
    }

    struct SpawnPoint
    {
        public Transform Transform;
        public bool IsEnable;

        public SpawnPoint(Transform transform, bool isEnable)
        {
            this.Transform = transform;
            this.IsEnable = isEnable;
        }
    }
}

