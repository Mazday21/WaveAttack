using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private GameObject _prefab;
    private Queue<GameObject> _pool;

    private void Awake()
    {
        InitializePool(_pool);
    }

    protected void InitializePrefab(GameObject prefab, Queue<GameObject> pool)
    {
        GameObject spawned = Instantiate(prefab);
        pool.Enqueue(spawned.gameObject);
        _prefab = prefab;
    }
    
    protected Queue<GameObject> InitializePool(Queue<GameObject> pool)
    {
        return new Queue<GameObject>();
    }

    protected void GetOrInstantiateGameObject(out GameObject result, Queue<GameObject> pool)
    {
        if (!pool.TryDequeue(out result))
        {
            result = Instantiate(_prefab);
        }
    }

    protected GameObject Instantiate(GameObject prefab)
    {
        return Instantiate(prefab, _container.transform);
    }

    public virtual void ReturnGameObject(GameObject gameObject, Queue<GameObject> pool)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = _container.transform.position;
        pool.Enqueue(gameObject);
    }
}
