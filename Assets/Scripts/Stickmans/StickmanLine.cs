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

    public float PositionZ { get; private set; }

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
        _spawner.AddEmptyPointZ(PositionZ);
    }

    public void SetPositionZ(float positionZ)
    {
        PositionZ = positionZ;
    }
}
