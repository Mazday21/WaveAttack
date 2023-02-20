using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerFallings : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
    }
}
