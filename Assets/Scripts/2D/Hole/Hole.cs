using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private LineSpawner _lineSpawner;
    [SerializeField] private WeaponSpawner _weaponSpawner;
    [SerializeField] private float _delayToAnimation;

    private int _banana = 0;
    private int _pineapple = 1;
    private int _watermelon = 2;
    private int _axe = 3;
    private WaitForSeconds _waitForSeconds;

    private void Start()
    {
        _waitForSeconds = new WaitForSeconds(_delayToAnimation);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Fallings falling))
        {
            if(falling.TryGetComponent(out DoubleFalling Dfalling))
            {
                _player.ChangePower(_player.Power * 2);
            }
            else if (falling.TryGetComponent(out MinusFiveFalling M5falling))
            {
                _player.ChangePower(_player.Power - 3);
            }
            else if (falling.TryGetComponent(out HeartFalling Hfalling))
            {
                _lineSpawner.Spawn();
            }
            else if (falling.TryGetComponent(out BananaFalling Bfalling))
            {
                _weaponSpawner.ChangeWeaponIndex(_banana);
            }
            else if (falling.TryGetComponent(out PineappleFalling Pfalling))
            {
                _weaponSpawner.ChangeWeaponIndex(_pineapple);
            }
            else if (falling.TryGetComponent(out WatermelonFalling Wfalling))
            {
                _weaponSpawner.ChangeWeaponIndex(_watermelon);
            }
            else if (falling.TryGetComponent(out AxeFalling Afalling))
            {
                _weaponSpawner.ChangeWeaponIndex(_axe);
            }

            _lineSpawner.StartThrowAnimated();
            StartCoroutine(DelayAnimation());
            Destroy(falling.gameObject);
        }
    }

    private IEnumerator DelayAnimation()
    {
        yield return _waitForSeconds;
        _weaponSpawner.Spawn(_player.Power);
    }
}
