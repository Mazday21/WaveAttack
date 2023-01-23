using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out StickmanWave stickman))
        {
            stickman.Hit();
        }
    }
}
