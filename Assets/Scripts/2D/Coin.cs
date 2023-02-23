using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 _destination;
    private float _speed = 3;
    private CoinPool _coinPool;
    private float _offset = 0.5f;
    private float _offsetCamera = 2f;

    private void Awake()
    {
        if (_coinPool is null)
        {
            _coinPool = GetComponentInParent<CoinPool>();
        }
    }

    private void OnEnable()
    {
        _destination = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, _offsetCamera));
    }

    private void Update()
    {
        if(Time.timeScale != 1)
        {
            _destination = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight, _offsetCamera));
        }

        transform.position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);

        if ((_destination - transform.position).sqrMagnitude < _offset)
        {
            _coinPool.ReturnGameObject(gameObject);
        }
    }
}
