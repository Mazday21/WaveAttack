using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickmanLine : Stickman
{
    private readonly int _hashAnimThrow = Animator.StringToHash("Threw");

    private void Awake()
    {
        LineSpawner spawner = transform.parent.gameObject.GetComponent<LineSpawner>();
        _spawner = spawner;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out StickmanWave stickman))
        {
            Hit();
            PassEmptyPointZ();
        }
    }

    public void Throw()
    {
        _animator.SetTrigger(_hashAnimThrow);
    }

    public void PassEmptyPointZ()
    {
        LineSpawner lineSpawner = (LineSpawner)_spawner;
        lineSpawner.AddEmptyPointZ(transform.position.z);
    }
}
