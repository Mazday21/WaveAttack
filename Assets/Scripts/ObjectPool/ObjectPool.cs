using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] protected GameObject _prefab;

    protected Queue<GameObject> _pool = new Queue<GameObject>();

    public void InitializePrefab(GameObject prefab)
    {
        GameObject spawned = Instantiate(prefab, transform);
        _prefab = prefab;
        _pool.Enqueue(spawned.gameObject);
        spawned.SetActive(false); 
    }

    public virtual void GetOrInstantiateGameObject(out GameObject result)
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
