using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;

    private Queue<GameObject> _pool = new Queue<GameObject>();

    public void InitializePrefab()
    {
        GameObject spawned = Instantiate(_prefab, transform);
        _pool.Enqueue(spawned.gameObject);
    }

    public void GetOrInstantiateGameObject(out GameObject result)
    {
        if (!_pool.TryDequeue(out result))
        {
            result = Instantiate(_prefab, transform);
        }
    }

    public void ReturnGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _pool.Enqueue(gameObject);
    }
}
