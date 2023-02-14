using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class LineSpawner : StickmanSpawner
{
    [SerializeField] private StickmanLine _linePrefab;
    [SerializeField] private Transform[] _points;
    [SerializeField] private WeaponSpawner _weaponSpawner;

    private SpawnPoint[] _spawnPoints;
    private int _countEmptyPoints;
    private Dictionary<float, StickmanLine> _lineStickmans = new Dictionary<float, StickmanLine>();

    public bool AllowSpawn { get; private set; }

    private void Start()
    {        
        _spawnPoints = new SpawnPoint[_points.Length];

        for (int i = 0; i < _points.Length; i++)
        {
            _spawnPoints[i] = new SpawnPoint(_points[i], false);
        }

        foreach(SpawnPoint point in _spawnPoints)
        {
            Pool.GetOrInstantiateGameObject(out GameObject lineStickman);
            StickmanLine stickmanLine = lineStickman.GetComponent<StickmanLine>();
            stickmanLine.SetPositionZ(point.Transform.position.z);
            SetStickman(lineStickman, point.Transform.position);
            _lineStickmans.Add(point.Transform.position.z, stickmanLine);
            var animator = lineStickman.GetComponent<Animator>();
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
                _lineStickmans.Remove(positionZ);

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
                _lineStickmans.Add(_spawnPoints[i].Transform.position.z, stickmanLine);
                _spawnPoints[i].IsEnable = false;
                _countEmptyPoints--;

                if(_countEmptyPoints == 0)
                    AllowSpawn = false;
                break;
            }
        }
    }

    public void StartThrowAnimated()
    {
        if (_lineStickmans.Count > 0)
        {
            foreach (KeyValuePair<float, StickmanLine> entry in _lineStickmans)
            {
                entry.Value.Throw();
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

