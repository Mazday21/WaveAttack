using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _centerRotationFrom;
    [SerializeField] private GameObject _centerRotationTo;
    [SerializeField] private float _speed;
    [SerializeField] private Power _power;

    private Vector3 _startPosition;
    private Camera _camera;
    private Vector3 _center;
    private Vector3 _centerTo;
    private float _time = 15;

    private void Start()
    {
        _startPosition = transform.position;
        _camera = GetComponent<Camera>();
        _center = _centerRotationFrom.transform.position;
        _centerTo = _centerRotationTo.transform.position;
        //StartCoroutine(SwitchCenter());
        //transform.DOLocalRotate(new Vector3(0, 360, 0), _time, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
        transform.DODynamicLookAt(_centerTo, _time);
        transform.DOMove(Vector3.forward * _speed * -1, _time);
    }

    private void Update()
    {
        //transform.LookAt(_center);
        //transform.Translate(Vector3.left * Time.deltaTime * _speed);
    }

    private IEnumerator SwitchCenter()
    {
        yield return new WaitForSeconds(6);
        _center = _centerTo;
    }

    private void Rotate()
    {
        //transform.LookAt(_center.transform);
        //transform.Translate(Vector3.left * Time.deltaTime * _speed);
        transform.DODynamicLookAt(_center, 30);
        //Time.timeScale = 0.1f;
        StartCoroutine(awaitRotate());
    }

    private IEnumerator awaitRotate()
    {
        yield return new WaitForSeconds(30f);
        Time.timeScale = 1;
    }

    //private void OnEnable()
    //{
    //    _power.PowerUp += Rotate;
    //}

    //private void OnDisable()
    //{
    //    _power.PowerUp -= Rotate;
    //}
}
