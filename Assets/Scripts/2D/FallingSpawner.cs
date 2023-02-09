using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    [SerializeField] private Fallings[] _fallingsWeapons;
    [SerializeField] private Fallings[] _fallingsBonuses;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private FollowCursor _followCursor;
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private FallingPool _fallingPool;

    private bool _coroutineAllowed = true;
    private Vector3 _leftSpawnPoint;
    private Vector3 _rightSpawnPoint;
    private Vector3 _firstSpawnPoint;
    private Vector3 _secondSpawnPoint;
    private float _offsetZ;
    private float _edgeOffset = 1;
    private Camera _camera;
    private bool _chooseWeapon = false;
    private GameObject _first;
    private GameObject _second;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _offsetZ = _followCursor.OffsetZ;
        _fallingPool.InitializePool(_fallingsWeapons, _fallingsBonuses);
    }

    private void Update()
    {
        if (_coroutineAllowed)
        {
            StartCoroutine(DelaySpawn());
        }
    }

    private IEnumerator DelaySpawn()
    {
        _coroutineAllowed = false;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);
        Spawn();
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    private void Spawn()
    {
        GetTerminalSpawnPoints();
        SetSpawnPoints();

        if (_chooseWeapon)
        {
            _first = _fallingPool.GetRandomWeapon();
            _second = _fallingPool.GetRandomWeapon();
            _chooseWeapon = false;
        }
        else
        {
            _first = _fallingPool.GetRandomBonus();
            _second = _fallingPool.GetRandomBonus();
        }

        _first.transform.position = _firstSpawnPoint;
        _second.transform.position = _secondSpawnPoint;
    }

    private void GetTerminalSpawnPoints()
    {
        _leftSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, _offsetZ));
        _rightSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _offsetZ));
    }

    private void SetSpawnPoints()
    {
        _firstSpawnPoint = new Vector3(Random.Range(_leftSpawnPoint.x + _edgeOffset, _rightSpawnPoint.x - _edgeOffset), _leftSpawnPoint.y, _leftSpawnPoint.z);
        _secondSpawnPoint = new Vector3(Random.Range(_leftSpawnPoint.x + _edgeOffset, _rightSpawnPoint.x - _edgeOffset), _leftSpawnPoint.y, _leftSpawnPoint.z);

        while(_secondSpawnPoint.z > _firstSpawnPoint.z - _edgeOffset && _secondSpawnPoint.z < _firstSpawnPoint.z + _edgeOffset)
        {
            _secondSpawnPoint = new Vector3(Random.Range(_leftSpawnPoint.x + _edgeOffset, _rightSpawnPoint.x - _edgeOffset), _leftSpawnPoint.y, _leftSpawnPoint.z);
        }
    }
}
