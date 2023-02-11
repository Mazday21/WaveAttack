using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LineSpawner : StickmanSpawner
{
    [SerializeField] private StickmanLine _linePrefab;
    [SerializeField] private Transform[] _points;
    [SerializeField] private WeaponSpawner _weaponSpawner;

    private SpawnPoint[] _spawnPoints;
    private int _countEmptyPoints;
    private List<StickmanLine> _lineStickmans = new List<StickmanLine>();

    public bool AllowSpawn { get; private set; }

    private void Start()
    {

        foreach(Transform point in _points)
        {
            Pool.GetOrInstantiateGameObject(out GameObject lineStickman);
            StickmanLine stickmanLine = lineStickman.GetComponent<StickmanLine>();
            stickmanLine.SetPositionZ(point.position.z);
            SetStickman(lineStickman, point.position);
            _lineStickmans.Add(lineStickman.GetComponent<StickmanLine>());
            var animator = lineStickman.GetComponent<Animator>();
        }
        _spawnPoints = new SpawnPoint[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            _spawnPoints[i] = new SpawnPoint(_points[i], false);
        }
    }

    public void AddEmptyPointZ(float positionZ)
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if(_spawnPoints[i].Transform.position.z == positionZ)
            {
                _spawnPoints[i].IsEnable = true;
                _weaponSpawner.ChangeConditionSpawnPoint(positionZ, false);
                break;
            }
        }
        _countEmptyPoints++;
        AllowSpawn = true;
    }

    public void Spawn()
    {
        for(int i = 0;i < _spawnPoints.Length; i++)
        {
            if (_spawnPoints[i].IsEnable)
            {
                _weaponSpawner.ChangeConditionSpawnPoint(_spawnPoints[i].Transform.position.z, true);
                Pool.GetOrInstantiateGameObject(out GameObject lineStickman);
                StickmanLine stickmanLine = lineStickman.GetComponent<StickmanLine>();
                stickmanLine.SetPositionZ(_spawnPoints[i].Transform.position.z);
                SetStickman(lineStickman, _spawnPoints[i].Transform.position);
                _lineStickmans.Add(lineStickman.GetComponent<StickmanLine>());
                _spawnPoints[i].IsEnable = false;
                _countEmptyPoints--;

                if (_countEmptyPoints == 0)
                    AllowSpawn = false;
                //break;
            }
        }
    }

    public void StartThrowAnimated()
    {
        if(_lineStickmans.Count > 0)
            _lineStickmans.ForEach(lineStickman => lineStickman.Throw());
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

