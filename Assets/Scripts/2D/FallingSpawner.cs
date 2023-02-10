using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private FollowCursor _followCursor;
    [SerializeField] private Fallings[] _fallingsWeapons;
    [SerializeField] private Fallings[] _fallingsBonuses;
    [SerializeField] private Camera _camera;
    [SerializeField] private LineSpawner _lineSpawner;

    private bool _coroutineAllowed = true;
    private Vector3 _leftSpawnPoint;
    private Vector3 _rightSpawnPoint;
    private Vector3 _firstSpawnPoint;
    private Vector3 _secondSpawnPoint;
    private float _offsetZ = 6;
    private float _edgeOffset = 1;
    private bool _chooseWeapon = false;
    private GameObject _first;
    private GameObject _second;
    private WaitForSeconds _waitForSeconds;
    private int _minusFive = 0;
    private int _double = 1;
    private int _heart = 2;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);  
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
        Spawn();
        yield return _waitForSeconds;
        _coroutineAllowed = true;
    }

    private void Spawn()
    {
        GetTerminalSpawnPoints();
        SetSpawnPoints();

        if (_chooseWeapon)
        {
            int firstRandomWeapon = Random.Range(0, _fallingsWeapons.Length);
            int secondRandomWeapon = Random.Range(0, _fallingsWeapons.Length);

            while (secondRandomWeapon == firstRandomWeapon)
                secondRandomWeapon = Random.Range(0, _fallingsWeapons.Length);

            _first = Instantiate(_fallingsWeapons[firstRandomWeapon].gameObject, transform);
            _second = Instantiate(_fallingsWeapons[secondRandomWeapon].gameObject, transform);
            _chooseWeapon = false;
        }
        else
        {
            if (_lineSpawner.AllowSpawn)
            {
            _first = Instantiate(_fallingsBonuses[Random.Range(_minusFive, _heart)].gameObject, transform);
            _second = Instantiate(_fallingsBonuses[_heart].gameObject, transform);

            }
            else
            {
                _first = Instantiate(_fallingsBonuses[_minusFive].gameObject, transform);
                _second = Instantiate(_fallingsBonuses[_double].gameObject, transform);
            }
            _chooseWeapon = true;
        }
        _first.transform.position = _firstSpawnPoint;
        _second.transform.position = _secondSpawnPoint;
    }

    private void GetTerminalSpawnPoints()
    {
        _leftSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, _offsetZ));
        _rightSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _offsetZ));
    }

    private void SetSpawnPoints()
    {
        _firstSpawnPoint = new Vector3( _leftSpawnPoint.x, _leftSpawnPoint.y, Random.Range(_leftSpawnPoint.z + _edgeOffset, _rightSpawnPoint.z - _edgeOffset));
        _secondSpawnPoint = new Vector3(_leftSpawnPoint.x, _leftSpawnPoint.y, Random.Range(_leftSpawnPoint.z + _edgeOffset, _rightSpawnPoint.z - _edgeOffset));

        while (_secondSpawnPoint.z > _firstSpawnPoint.z - _edgeOffset && _secondSpawnPoint.z < _firstSpawnPoint.z + _edgeOffset)
        {
            _secondSpawnPoint = new Vector3(_leftSpawnPoint.x, _leftSpawnPoint.y, Random.Range(_leftSpawnPoint.z + _edgeOffset, _rightSpawnPoint.z - _edgeOffset));
        }
    }
}
