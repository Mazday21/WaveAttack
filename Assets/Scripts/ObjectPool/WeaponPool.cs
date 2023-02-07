using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPool : ObjectPool
{
    [SerializeField] private WeaponSpawner weaponSpawner;

    public override void GetOrInstantiateGameObject(out GameObject result)
    {
        if (_pool.TryDequeue(out result))
        { 
            if (!result.name.StartsWith(_prefab.name))
            {
                result = Instantiate(_prefab, transform);
            }
        }
        else result = Instantiate(_prefab, transform);
    }
}
