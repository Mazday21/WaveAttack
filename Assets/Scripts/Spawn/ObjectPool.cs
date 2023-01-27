using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;

    private GameObject _prefab;
    private Queue<GameObject> _pool = new Queue<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab);
        _pool.Enqueue(spawned);
        _prefab = prefab;
    }

    protected void GetOrInstantiateGameObject(out GameObject result)
    {
        if (!_pool.TryDequeue(out result))
        {
            result = Instantiate(_prefab);
        }
    }

    protected GameObject Instantiate(GameObject prefab)
    {
        return Instantiate(prefab, _container.transform);
    }

    public virtual void ReturnGameObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
        gameObject.transform.position = _container.transform.position;
        _pool.Enqueue(gameObject);
    }
}
