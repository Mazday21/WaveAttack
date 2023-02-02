using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : ObjectPool
{
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpwardForce;
    [SerializeField] private float _maxSpreadY;
    [SerializeField] private float _maxSpreadZ;
    [Range(_minPower, _maxPower)][SerializeField] private int _power;
    [SerializeField] private int _currentWeaponIndex;
    [SerializeField] private float _secondsBetweenSpawn;

    private bool _coroutineAllowed = true;
    private Vector3[] _quaternionWeapon;
    private SpawnPoint[] _spawnPoints;
    private float _ratio;
    private const int _maxPower = 20;
    private const int _minPower = 6;
    public Queue<GameObject> Pool;

    private void Awake()
    {
        InitializePool(Pool);
    }

    private void Start()
    {
        _spawnPoints = new SpawnPoint[_points.Length];

        for(int i = 0; i < _points.Length; i++)
        {
            _spawnPoints[i] = new SpawnPoint(_points[i], true);
        }

        _quaternionWeapon = new Vector3[_weapons.Length];

        for (int i = 0; i < _weapons.Length; i++)
        {
            Transform transform = _weapons[i].GetComponent<Transform>();
            _quaternionWeapon[i] = transform.rotation.eulerAngles;
        }

        ChangeWeapon();
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
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);
        Spawn(_power);
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    private void ChangeWeapon()
    {
        InitializePrefab(_weapons[_currentWeaponIndex].gameObject);
    }

    public void ChangeWeaponIndex(int index)
    {
        if (index < 0 || index >= _weapons.Length)
        {
            throw new System.ArgumentException(index + " goes beyond");
        }
        _currentWeaponIndex = index;
        ChangeWeapon();
    }

    public void Spawn(int power)
    {
        if (power > 0 || power <= _maxPower)
        {
            _ratio = power / (float)_maxPower;
            for (int i = 1; i <= power; i++)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    if (spawnPoint.IsEnable)
                    {
                        GetOrInstantiateGameObject(out GameObject weapon, Pool);
                        SetWeapon(spawnPoint.Transform, weapon);
                        SetDirectionThrow(spawnPoint.Transform, weapon);
                    }
                }
            }
        }
        else throw new System.ArgumentException("Wrong power " + power);
    }

    private void SetDirectionThrow(Transform spawnPoint, GameObject weapon)
    {
        Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
        Vector3 straightDirection = spawnPoint.transform.right * -1;
        float spreadY = (_maxSpreadY / 2 - Random.Range(0.0f, _maxSpreadY)) * _ratio;
        Vector3 forceDirection = new Vector3(straightDirection.x, straightDirection.y - spreadY, straightDirection.z - (_maxSpreadZ / 2 - Random.Range(0.0f, _maxSpreadZ))*_ratio).normalized;
        Vector3 forceToAdd = forceDirection * _throwForce + transform.up * _throwUpwardForce;
        weaponRb.AddForce(forceToAdd, ForceMode.Impulse);
    }

    private void SetWeapon(Transform spawnPoint, GameObject gameObject)
    {
        gameObject.SetActive(true);
        gameObject.transform.rotation = Quaternion.Euler(_quaternionWeapon[_currentWeaponIndex]);
        gameObject.transform.position = spawnPoint.transform.position;
    }

    public void ChangeConditionSpawnPoint(float positionZ, bool condition)
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if (_spawnPoints[i].Transform.position.z == positionZ)
            {
                _spawnPoints[i].IsEnable = condition;
                 break;
            }
        }
    }

    public override void ReturnGameObject(GameObject gameObject, Queue<GameObject> pool)
    {
        base.ReturnGameObject(gameObject, Pool);
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
