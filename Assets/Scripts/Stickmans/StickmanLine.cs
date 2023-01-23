using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickmanLine : Stickman
{
    private readonly int _hashAnimThrow = Animator.StringToHash("Threw");

    public void Throw()
    {
        _animator.SetTrigger(_hashAnimThrow);
    }

    public void Death()
    {
        LineSpawner lineSpawner = (LineSpawner)_spawner;
        lineSpawner.AddEmptyPoint(transform.position.z);
    }
}
