using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Stickman
{
    [SerializeField] private float _speed;

    private int _additionalHealthForEnemy = 1;

    public int Reward { get; protected set; } = 1;

    protected virtual void Awake()
    {
        StartHealth += _additionalHealthForEnemy;
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

    protected override void GetDeath()
    {
        if(Time.timeScale != 1)
        {
            Player.ScoreChangeCalled(Reward);
        }
        else
        {
            ScoreQueue.ScoreChangeCalled(Reward);
            Spawner.RayCasting(transform.position);
        }
        base.GetDeath();
    }
}
