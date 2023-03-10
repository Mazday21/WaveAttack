using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpwardForce;
    [SerializeField] private float _maxSpreadY;
    [SerializeField] private float _maxSpreadZ;
    [SerializeField] private ObjectPool _pool;

    private int _currentWeaponIndex;
    private Vector3[] _quaternionWeapon;
    private SpawnPoint[] _spawnPoints;
    private float _ratio;
    private const int _maxPower = 20;
    private const int _minPower = 3;

    private void Awake()
    {
        ChangeWeapon();
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
    }

    private void ChangeWeapon()
    {
        _pool.InitializePrefab(_weapons[_currentWeaponIndex].gameObject);
    }

    public void ChangeWeaponIndex(WeaponFallings weapon)
    {
        for(int i = 0; i < _weapons.Length; i++)
        {
            if (weapon.name.StartsWith(_weapons[i].name))
            {
                _currentWeaponIndex = i;
                break;
            }
        }
        ChangeWeapon();
    }

    public void Spawn(int power)
    {
        if (power > _minPower || power <= _maxPower)
        {
            _ratio = power / (float)_maxPower;
            for (int i = 1; i <= power; i++)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    if (spawnPoint.IsEnable)
                    {
                        _pool.GetOrInstantiateGameObject(out GameObject weapon);
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
