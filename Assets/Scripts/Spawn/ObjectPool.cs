using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private GameObject _prefab;
    private Queue<GameObject> Pool;

    private void Awake()
    {
        InitializePool(Pool);
    }

    protected void InitializePrefab(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab);
        Pool.Enqueue(spawned);
        _prefab = prefab;
    }
    
    protected void InitializePool<T>(Queue<T> Pool)
    {
        Pool = new Queue<T>();
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
