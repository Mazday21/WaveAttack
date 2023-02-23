using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Stickman : MonoBehaviour
{
    [SerializeField] private float _secondsToDisappear;

    protected StickmanSpawner Spawner;
    protected Animator Animator;

    private readonly int _hashAnimDead = Animator.StringToHash("IsDead");
    private bool _coroutineAllowed = true;

    public bool IsAlive { get; private set; } = true;
    public int Health { get; protected set; } 
    public int StartHealth { get; protected set; } = 1;

    private void Start()
    {
        if (Animator == null)
            Animator = GetComponent<Animator>();

        Health = StartHealth;
    }

    public virtual void Hit(int damage)
    {
        if (IsAlive)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Animator.SetBool(_hashAnimDead, true);
                Collider collider = GetComponent<Collider>();
                collider.enabled = false;
                IsAlive = false;
                Health = StartHealth;

                if (_coroutineAllowed)
                {
                    StartCoroutine(DelayVanishing());
                }
            }
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

    public void SetAlive()
    {
        IsAlive = true;
    }
}
