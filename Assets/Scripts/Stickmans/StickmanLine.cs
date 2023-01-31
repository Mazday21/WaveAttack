using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickmanLine : Stickman
{
    private readonly int _hashAnimThrow = Animator.StringToHash("Threw");

    //public int Number
    //{
    //    get { return Number; }
    //    set { if (value > 0 || value < 6)
    //        {
    //            Number = value;
    //        }
    //    }
    //}

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
        lineSpawner.AddEmptyPointZ((float)System.Math.Round(transform.position.z, 1));
    }
}
