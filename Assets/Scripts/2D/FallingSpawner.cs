using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FallingSpawner : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Fallings[] _fallingsWeapons;
    [SerializeField] private Fallings[] _fallingsBonuses;
    [SerializeField] private Camera _camera;
    [SerializeField] private LineSpawner _lineSpawner;

    private bool _coroutineAllowed = true;
    private Vector3 _firstSpawnPoint;
    private Vector3 _secondSpawnPoint;
    private float _offset = 1.4f;
    private bool _chooseWeapon = false;
    private GameObject _first;
    private GameObject _second;
    private WaitForSeconds _waitForSpawn;
    private int _plusFive = 0;
    //private int _minusFive = 1;
    //private int _double = 2;
    private int _heart = 3;
    private float _randomX1;
    private float _randomX2;
    private float _center = 11.5f;
    private Vector3 _leftSpawnPoint;
    private Vector3 _rightSpawnPoint;

    private void Start()
    {
        _waitForSpawn = new WaitForSeconds(_secondsBetweenSpawn);  
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
        yield return _waitForSpawn;
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
            _first = Instantiate(_fallingsBonuses[Random.Range(_plusFive, _heart)].gameObject, transform);
            _second = Instantiate(_fallingsBonuses[_heart].gameObject, transform);

            }
            else
            {
                _first = Instantiate(_fallingsBonuses[Random.Range(_plusFive, _heart)].gameObject, transform);
                _second = Instantiate(_fallingsBonuses[Random.Range(_plusFive, _heart)].gameObject, transform);
            }
            _chooseWeapon = true;
        }
        _first.transform.position = _firstSpawnPoint;
        _second.transform.position = _secondSpawnPoint;
    }

    private void SetSpawnPoints()
    {
        _leftSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, _offset)) + new Vector3(_offset, 0,0);
        _rightSpawnPoint = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _offset)) - new Vector3(_offset, 0, 0);
        _randomX1 = Random.Range(_leftSpawnPoint.x , _rightSpawnPoint.x );

        if(_randomX1 > _center)
        {
            _randomX2 = Random.Range(_leftSpawnPoint.x, _randomX1 - _offset);
        }
        else
            _randomX2 = Random.Range(_randomX1 + _offset, _rightSpawnPoint.x);

        _firstSpawnPoint = new Vector3(_randomX1, _leftSpawnPoint.y, _leftSpawnPoint.z);
        _secondSpawnPoint = new Vector3(_randomX2, _leftSpawnPoint.y, _leftSpawnPoint.z);
    }
}
