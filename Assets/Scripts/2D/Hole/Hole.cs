using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private LineSpawner _lineSpawner;
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private float _delayToAnimation;

    private WaitForSeconds _waitForAnimation;
    private WaitForSeconds _waitToCollision;
    private float _delayToCol = 2;
    private bool _isActive = true;
    private ParticleSystem _particle;
    private ParticleSystem.MainModule _particleSettings;

    private void Start()
    {
        _waitForAnimation = new WaitForSeconds(_delayToAnimation);
        _waitToCollision = new WaitForSeconds(_delayToCol);
        _particle = GetComponentInChildren<ParticleSystem>();
        _particleSettings = _particle.main;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Fallings falling))
        {
            if(_isActive)
            {
                _isActive = false;

                if (falling.TryGetComponent(out PowerChanges change))
                {
                    _power.ChangePower(change);
                }
                else if (falling.TryGetComponent(out HeartFalling Hfalling))
                {
                    _lineSpawner.Spawn();
                }
                else if (falling.TryGetComponent(out WeaponFallings weapon))
                {
                    _weaponSpawner.ChangeWeaponIndex(weapon);
                }
                _particleSettings.startColor = new ParticleSystem.MinMaxGradient(falling.GetColor());
                _particle.Play();
                _lineSpawner.StartThrowAnimated();
                StartCoroutine(DelayAnimation());
                Destroy(falling.gameObject);
                StartCoroutine(DelayActive());
            }
        }
    }

    private IEnumerator DelayActive()
    {
        yield return _waitToCollision;
        _isActive = true;
    }

    private IEnumerator DelayAnimation()
    {
        yield return _waitForAnimation;
        _weaponSpawner.Spawn(_power.Value);
    }
}
