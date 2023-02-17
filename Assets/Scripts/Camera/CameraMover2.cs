using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class CameraMover2 : MonoBehaviour
{
    [SerializeField] private GameObject _centerRotationFrom;
    [SerializeField] private GameObject[] _objectWayPoints;
    [SerializeField] private Power _power;

    private bool _lookFollow = false;
    private float _timeLookAt = 3;
    private int _timeRotation = 2;
    private Vector3 _startPosition;
    private Vector3[] _wayPoints;
    private PathType _pathType = PathType.CatmullRom;
    private PathMode _pathMode = PathMode.Full3D;
    private Color _color = Color.red;
    private float _fixedDeltaTime;

    private void Awake()
    {
        this._fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Start()
    {
        _startPosition = transform.position;
        _wayPoints = new Vector3[_objectWayPoints.Length];

        for(int i = 0; i < _objectWayPoints.Length; i++)
        {
            _wayPoints[i] = _objectWayPoints[i].transform.position;
        }
    }

    private void Update()
    {
        if(_lookFollow) 
        {
            gameObject.transform.LookAt(_centerRotationFrom.transform.position);
            Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        }
    }

    private void Rotation()
    {
        _lookFollow = true;
        StartCoroutine(DelayToRotate());
        transform.DOPath(_wayPoints, _timeRotation, _pathType, _pathMode, 5, _color);
    }

    private IEnumerator DelayToRotate()
    {
        yield return new WaitForSeconds(_timeLookAt);
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
