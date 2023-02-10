using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fallings : MonoBehaviour
{
    private float _speed = 1;

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        transform.Translate(Vector3.up * -1 * _speed * Time.deltaTime);
    }
}
