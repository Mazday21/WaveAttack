using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Splines : MonoBehaviour
{
    [SerializeField] private Power _power;
    [SerializeField] private GameObject _centerRotation;

    private bool _isRotate = false;
    private float _fixedDeltaTime;

    private void Awake()
    {
        this._fixedDeltaTime = Time.fixedDeltaTime;
    }

    private void Update()
    {
        if(_isRotate)
        {
            transform.Translate(Vector3.left);
            //Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            //if (Vector3.Distance(transform.position, _centerRotation.transform.position) < 12f)
            //{
            //    transform.Translate(_centerRotation.transform.position);
            //}
        }
    }

    private void Rotation()
    {
        _isRotate = true;
        StartCoroutine(DelayToStop());
    }

    private IEnumerator DelayToStop()
    {
        yield return new WaitForSeconds(2f);
        //transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        _isRotate = false;
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
