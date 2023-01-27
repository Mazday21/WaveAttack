using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _secondsToVanishing;
    [SerializeField] private Vector3 _Rotation;

    private WeaponSpawner _spawner;
    private bool _coroutineAllowed = true;

    private bool _isFallen = false;

    private void Awake()
    {
        WeaponSpawner spawner = transform.parent.gameObject.GetComponent<WeaponSpawner>();
        _spawner = spawner;
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

    private void OnTriggerEnter(Collider col)
    {
        if (!_isFallen)
        {
            if (col.TryGetComponent(out StickmanWave stickman))
            {
                stickman.Hit();
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
        _spawner.ReturnGameObject(gameObject);
        _isFallen = false;
        _coroutineAllowed = true;
    }
}
