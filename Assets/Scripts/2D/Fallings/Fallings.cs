using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Fallings : MonoBehaviour
{
    private float _speed = 4f;

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    public abstract Color GetColor();
}
