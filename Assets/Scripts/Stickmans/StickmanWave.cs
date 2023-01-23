using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class StickmanWave : Stickman
{
    [SerializeField] private float _speed;

    public event UnityAction StickmanCollided;

    private void Update()
    {
        if(IsAlive)
        {
            Run();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out StickmanLine stickman))
        {
            stickman.Hit();
            stickman.Death();
        }
    }

    private void Run()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }
}
