using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StickmanWave : Stickman
{
    [SerializeField] private float _speed;

    private void Awake()
    {
        WaveSpawner spawner = transform.parent.gameObject.GetComponent<WaveSpawner>();
        Spawner = spawner;
    }

    private void Update()
    {
        if(IsAlive)
        {
            Run();
        }
    }

    private void Run()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
