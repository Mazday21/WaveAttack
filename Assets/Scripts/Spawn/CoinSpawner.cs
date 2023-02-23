using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _collisionLayer;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinPool _pool;

    private void Awake()
    {
        _pool.InitializePrefab(_coinPrefab.gameObject);
    }

    public void RayCasting(Vector3 startPosition)
    {
        Ray ray = new Ray(startPosition, _camera.transform.position - startPosition);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, Mathf.Infinity, _collisionLayer);

        if (hitInfo.collider != null)
        {
            Vector3 spawnPosition = hitInfo.point;
            _pool.GetOrInstantiateGameObject(out GameObject coin);
            SetCoin(coin, spawnPosition);
        }
    }

    private void SetCoin(GameObject coin, Vector3 spawnPoint)
    {
        coin.SetActive(true);
        coin.transform.position = spawnPoint;
        coin.transform.rotation = _camera.transform.rotation;
    }
}

