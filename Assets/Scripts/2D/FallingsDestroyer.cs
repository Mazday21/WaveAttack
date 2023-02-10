using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingsDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.TryGetComponent(out Fallings falling))
        {
            Destroy(falling.gameObject);
        }
    }
}
