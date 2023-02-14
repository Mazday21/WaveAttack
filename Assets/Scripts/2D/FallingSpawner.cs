using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Fallings[] _fallingsWeapons;
    [SerializeField] private Fallings[] _fallingsBonuses;
    [SerializeField] private Camera _camera;
    [SerializeField] private LineSpawner _lineSpawner;
    [SerializeField] private GameObject _leftSpawnPoint;
    [SerializeField] private GameObject _rightSpawnPoint;

    private bool _coroutineAllowed = true;
    private Vector3 _firstSpawnPoint;
    private Vector3 _secondSpawnPoint;
    private float _offset = 2;
    private bool _chooseWeapon = false;
    private GameObject _first;
    private GameObject _second;
    private WaitForSeconds _waitForSeconds;
    private int _minusFive = 0;
    private int _double = 1;
    private int _heart = 2;
    private float _randomX1;
    private float _randomX2;

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

    private void SetSpawnPoints()
    {
        _randomX1 = Random.Range(_leftSpawnPoint.transform.position.x , _rightSpawnPoint.transform.position.x );
        _randomX2 = Random.Range(_leftSpawnPoint.transform.position.x , _rightSpawnPoint.transform.position.x );

        while (_randomX2 > _randomX1 - _offset && _randomX2 < _randomX1 + _offset)
        {
            _randomX2 = Random.Range(_leftSpawnPoint.transform.position.z, _rightSpawnPoint.transform.position.z);
        }
        _firstSpawnPoint = new Vector3(_randomX1, _leftSpawnPoint.transform.position.y, _leftSpawnPoint.transform.position.z);
        _secondSpawnPoint = new Vector3(_randomX2, _leftSpawnPoint.transform.position.y, _leftSpawnPoint.transform.position.z);
    }

}
