using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    private ParticleSystem _particle;
    private int _additionalHealthForBig = 1;
    private int _additionalReward = 1;

    protected override void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        Reward += _additionalReward;
        StartHealth += _additionalHealthForBig;
    }

    protected override void GetDeath()
    {
        _particle.Play();
        base.GetDeath();
    }
}
