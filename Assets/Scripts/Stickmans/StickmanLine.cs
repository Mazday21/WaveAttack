using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StickmanLine : Stickman
{
    private LineSpawner _spawner;
    private readonly int _hashAnimThrow = Animator.StringToHash("Threw");

    private void Awake()
    {
        LineSpawner spawner = GetComponentInParent<LineSpawner>();
        _spawner = spawner;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Enemy stickman))
        {
            Hit();
            PassEmptyPointZ();
        }
    }

    public void Throw()
    {
        Animator.SetTrigger(_hashAnimThrow);
    }

    public void PassEmptyPointZ()
    {
        _spawner.AddEmptyPointZ((float)System.Math.Round(transform.position.z, 1));
    }
}
