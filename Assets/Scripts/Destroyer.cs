using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private int _damage = 3;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Enemy stickman))
        {
            stickman.Hit(_damage);
        }
    }
}
