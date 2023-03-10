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
    private int _damage = 1;

    public Vector3 Position { get; private set; }

    private void Awake()
    {
        LineSpawner spawner = GetComponentInParent<LineSpawner>();
        _spawner = spawner;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Enemy stickman))
        {
            Hit(_damage);
            PassEmptyPointZ();
            transform.position += new Vector3(0, -0.24f, 0);
        }
    }

    public void Throw()
    {
        Animator.SetTrigger(_hashAnimThrow);
        transform.position = Position;
    }

    public void PassEmptyPointZ()
    {
        _spawner.AddEmptyPointZ(Position.z);
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
}
