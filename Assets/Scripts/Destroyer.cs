using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Enemy stickman))
        {
            stickman.Hit();
        }
    }
}
