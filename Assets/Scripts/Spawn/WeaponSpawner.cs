using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : ObjectPool
{
    [SerializeField] private Weapon[] weapons;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private bool _coroutineAllowed = true;

    private void Start()
    {
        Initialize(weapons[0].gameObject);
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
        Spawn(1);
        yield return waitForSeconds;
        _coroutineAllowed = true;
    }

    public void Spawn(int strength)
    {
        if(strength > 0 || strength < 100)
        {
            foreach(var spawnPoint in _spawnPoints)
            {
                GetOrInstantiateGameObject(out GameObject weapon);
                weapon.transform.position = spawnPoint.transform.position;
                Rigidbody weaponRb = weapon.GetComponent<Rigidbody>();
                Vector3 forceDirection = spawnPoint.transform.right * -1;
                Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;
                weaponRb.AddForce(forceToAdd, ForceMode.Impulse);
            }
        }
    }
}
