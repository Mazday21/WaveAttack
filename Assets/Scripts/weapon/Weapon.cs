using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _secondsToVanishing;
    [SerializeField] private Vector3 _Rotation;

    private ObjectPool _spawner;
    private bool _coroutineAllowed = true;

    public bool Shutdown { get; private set; } = false;

    private void Awake()
    {
        ObjectPool spawner = transform.parent.gameObject.GetComponent<WeaponSpawner>();
        _spawner = spawner;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (!Shutdown)
        {
            transform.Rotate(_Rotation * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!Shutdown)
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
            Shutdown = true;
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
        _coroutineAllowed = true;
    }
}
