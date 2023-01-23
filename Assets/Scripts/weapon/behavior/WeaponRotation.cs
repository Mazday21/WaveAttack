using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField] private Vector3 _Rotation;

    private bool _IsRotating = true;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out Track track) || (col.TryGetComponent(out StickmanWave stickman)))
        {
            _IsRotating = false;
        }
    }

    private void Rotate(bool isRotating)
    {
        if (_IsRotating)
        {
            transform.Rotate(_Rotation * Time.deltaTime);
        }
    }

    private void Update()
    {
       Rotate(_IsRotating);
    }
}
