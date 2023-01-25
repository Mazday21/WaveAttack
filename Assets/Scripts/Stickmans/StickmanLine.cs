using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickmanLine : Stickman
{
    private readonly int _hashAnimThrow = Animator.StringToHash("Threw");

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out StickmanWave stickman))
        {
            Hit();
            Death();
        }
    }

    public void Throw()
    {
        _animator.SetTrigger(_hashAnimThrow);
    }

    public void Death()
    {
        LineSpawner lineSpawner = (LineSpawner)_spawner;
        lineSpawner.AddEmptyPointZ(transform.position.z);
    }
}
