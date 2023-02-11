using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private LineSpawner _lineSpawner;
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private float _delayToAnimation;

    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_delayToAnimation);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Fallings falling))
        {
            if(falling.TryGetComponent(out PowerChanges change))
            {
                _power.ChangePower(change);
            }
            else if (falling.TryGetComponent(out HeartFalling Hfalling))
            {
                _lineSpawner.Spawn();
            }
            else if (falling.TryGetComponent(out Weapon weapon))
            {
                _weaponSpawner.ChangeWeaponIndex(weapon);
            }

            _lineSpawner.StartThrowAnimated();
            StartCoroutine(DelayAnimation());
            Destroy(falling.gameObject);
        }
    }

    private IEnumerator DelayAnimation()
    {
        yield return _waitForSeconds;
        _weaponSpawner.Spawn(_power.Value);
    }
}
