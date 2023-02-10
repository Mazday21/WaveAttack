using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stickman : MonoBehaviour
{
    [SerializeField] private float _secondsToDisappear;

    protected StickmanSpawner Spawner;
    protected Animator Animator;
    private readonly int _hashAnimDead = Animator.StringToHash("IsDead");
    private bool _coroutineAllowed = true;
    public bool IsAlive = true;

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    public void Hit()
    {
        Animator.SetBool(_hashAnimDead, true);
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
        Spawner.ReturnGameObject(gameObject);
        _coroutineAllowed = true;
    }

    public void Initialize(StickmanSpawner spawner)
    {
        Spawner = spawner;
    }
}
