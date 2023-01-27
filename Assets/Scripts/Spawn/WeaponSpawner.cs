using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSpawner : ObjectPool
{
    [SerializeField] private Weapon[] _weapons;
    //[SerializeField] private List<Weapon> _weapons;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _throwForce;
    [SerializeField] private float _throwUpwardForce;
    [SerializeField] private float _deltaThrowUpwardForce;
    [SerializeField] private float _spreadZ;

    private bool _coroutineAllowed = true;
    private float _minThrowUpwardForce;
    private float _maxThrowUpwardForce;
    private Weapon _currentWeapon;
    private Quaternion[] _quaternionsWeapon;

    private void Start()
    {
        _quaternionsWeapon = new Quaternion[_weapons.Length];
        ChangeWeapon(_weapons[3]);
        _minThrowUpwardForce = _throwUpwardForce - _deltaThrowUpwardForce / 2;
        _maxThrowUpwardForce = _throwUpwardForce + _deltaThrowUpwardForce / 2;

        for (int i = 0; i < _weapons.Length; i++)
        {
            _quaternionsWeapon[i] = _weapons[i].transform.rotation;
            Debug.Log(_weapons[i].transform.rotation.x + " " + _weapons[i].transform.rotation.y + " " + _weapons[i].transform.rotation.z);
            Debug.Log("---   ---");
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
        Spawn(3);
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        Initialize(_currentWeapon.gameObject);
    }

    public void Spawn(int strength)
    {
        if(strength > 0 || strength < 20)
        {
            for(int i = 0; i <= strength; i++)
            {
                foreach (var spawnPoint in _spawnPoints)
                {
                    GetOrInstantiateGameObject(out GameObject weapon);
                    weapon.SetActive(true);
                    weapon.transform.position = spawnPoint.transform.position;
                    Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
                    Vector3 straightDirection = spawnPoint.transform.right * -1;
                    Vector3 forceDirection = new Vector3(straightDirection.x, straightDirection.y, straightDirection.z - _spreadZ / 2 + Random.Range(0.0f, _spreadZ)).normalized;
                    float throwUpwardForce = Random.Range(_minThrowUpwardForce, _maxThrowUpwardForce);
                    Vector3 forceToAdd = forceDirection * _throwForce + transform.up * throwUpwardForce;
                    weaponRb.AddForce(forceToAdd, ForceMode.Impulse);
                }
            }
        }
    }

    public override void ReturnGameObject(GameObject gameObject)
    {
        base.ReturnGameObject(gameObject);
        Weapon weapon = gameObject.GetComponent<Weapon>();

        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] == weapon)
            {
                gameObject.transform.rotation = _quaternionsWeapon[i];
                break;
            }
        }
    }
    //public override void ReturnGameObject(GameObject gameObject)
    //{
    //    base.ReturnGameObject(gameObject);
    //    Weapon weapon = gameObject.GetComponent<Weapon>();
    //    Weapon weaponToReturn = _weapons.First(w => w == weapon);
    //    gameObject.transform.rotation = weaponToReturn.transform.rotation;
    //}
}
