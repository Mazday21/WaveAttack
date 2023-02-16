using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class UpDownCenter : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private GameObject _moveTo;

    private Vector3 _moveFrom;
    private float _rotateTime = 0.5f;
    private float _upDownTime = 0.4f;

    private void Start()
    {
        _moveFrom = transform.position;
    }

    private void UpDown()
    {
        transform.DOMove(_moveTo.transform.position, _upDownTime);
        StartCoroutine(Down());
    }

    private IEnumerator Down()
    {
        yield return new WaitForSeconds(_rotateTime);
        transform.DOMove(_moveFrom, _upDownTime);
    }

    private void OnEnable()
    {
        _power.PowerUp += UpDown;
    }

    private void OnDisable()
    {
        _power.PowerUp -= UpDown;
    }
}
