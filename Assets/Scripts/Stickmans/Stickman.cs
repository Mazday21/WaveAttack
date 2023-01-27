using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stickman : MonoBehaviour
{
    [SerializeField] private float _secondsToDisappear;

    protected StickmanSpawner _spawner;
    protected Animator _animator;
    private readonly int _hashAnimDead = Animator.StringToHash("IsDead");
    private bool _coroutineAllowed = true;
    public bool IsAlive = true;

    public void Hit()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(_hashAnimDead, true);
        Collider collider = GetComponent<Collider>();
        collider.enabled = false;
        IsAlive = false;

        if (_coroutineAllowed)
        {
            StartCoroutine(DelayVanishing());
        }
    }

    private IEnumerator DelayVanishing()
    {
        _coroutineAllowed = false;
        WaitForSeconds waitForSeconds = new WaitForSeconds(_secondsToDisappear);
        yield return waitForSeconds;
        _spawner.ReturnGameObject(gameObject);
        _coroutineAllowed = true;
    }
}
