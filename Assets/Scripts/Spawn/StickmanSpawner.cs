using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanSpawner : ObjectPool
{
    protected void SetStickman(GameObject Stickman, Vector3 spawnPoint)
    {
        Stickman.SetActive(true);
        Stickman stickman = Stickman.GetComponent<Stickman>();
        stickman.IsAlive = true;
        Stickman.transform.position = spawnPoint;
    }

    public override void ReturnGameObject(GameObject gameObject)
    {
        base.ReturnGameObject(gameObject);
        Collider collider = gameObject.GetComponent<Collider>();
        collider.enabled = true;
    }
}
