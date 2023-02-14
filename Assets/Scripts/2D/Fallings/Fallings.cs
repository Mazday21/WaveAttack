using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fallings : MonoBehaviour
{
    private float _speed = 4f;
    private float _secondsToDestroy = 3f;

    private void Start()
    {
        Destroy(gameObject, _secondsToDestroy);
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
