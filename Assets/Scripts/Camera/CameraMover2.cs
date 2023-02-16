using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraMover2 : MonoBehaviour
{
    [SerializeField] private GameObject _centerRotationFrom;
    [SerializeField] private Power _power;

    private bool _lookFollow = false;
    private float _timeRotation = 3;

    private void Update()
    {
        if(_lookFollow) 
        {
            gameObject.transform.LookAt(_centerRotationFrom.transform.position);
        }
    }

    private void Rotation()
    {
        _lookFollow = true;
        StartCoroutine(DelayToRotate());
    }

    private IEnumerator DelayToRotate()
    {
        yield return new WaitForSeconds(_timeRotation);
        _lookFollow = false;
    }

    private void OnEnable()
    {
        _power.PowerUp += Rotation;
    }

    private void OnDisable()
    {
        _power.PowerUp -= Rotation;
    }
}
