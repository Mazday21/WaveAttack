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
        Spawner = spawner;
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
        Animator.SetTrigger(_hashAnimThrow);
    }

    public void PassEmptyPointZ()
    {
        LineSpawner lineSpawner = (LineSpawner)Spawner;
        lineSpawner.AddEmptyPointZ((float)System.Math.Round(transform.position.z, 1));
    }
}
