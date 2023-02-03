using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanSpawner : MonoBehaviour
{
    [SerializeField] protected ObjectPool Pool;

    protected void SetStickman(GameObject Stickman, Vector3 spawnPoint)
    {
        Stickman stickman = Stickman.GetComponent<Stickman>();
        stickman.Initialize(this);
        Stickman.SetActive(true);
        stickman.IsAlive = true;
        Stickman.transform.position = spawnPoint;
    }

    public virtual void ReturnGameObject(GameObject gameObject)
    {
        Pool.ReturnGameObject(gameObject);
        Collider collider = gameObject.GetComponent<Collider>();
        collider.enabled = true;
    }
}
