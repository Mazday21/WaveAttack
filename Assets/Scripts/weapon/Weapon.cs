using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _secondsToVanishing;
    [SerializeField] private Vector3 _Rotation;

    private WeaponPool _weaponPool;
    private bool _coroutineAllowed = true;
    private bool _isFallen = false;
    protected int _damage = 1;

    private void Awake()
    {
        if (_weaponPool is null)
        {
            _weaponPool = GetComponentInParent<WeaponPool>();
        }
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!_isFallen)
        {
            transform.Rotate(_Rotation * Time.deltaTime);
        }
    }

    public void SetWeaponPool(WeaponPool weaponPool)
    {
        _weaponPool = weaponPool;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!_isFallen)
        {
            if (col.TryGetComponent(out Enemy stickman))
            {
                stickman.Hit(_damage);

                if (_coroutineAllowed)
                {
                    StartCoroutine(DelayVanishing());
                }
            }
        }
        if (col.TryGetComponent(out Track track))
        {
            _isFallen = true;

            if (_coroutineAllowed)
            {
                StartCoroutine(DelayVanishing());
            }
        }
    }

    private IEnumerator DelayVanishing()
    {
        _coroutineAllowed = false;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsToVanishing);
        yield return waitForSeconds;
        _weaponPool.ReturnGameObject(gameObject);
        _isFallen = false;
        _coroutineAllowed = true;
    }
}
