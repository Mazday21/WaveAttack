using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    private ParticleSystem _particle;
    private int _additionalHealthForBig = 1;
    private int _additionalReward = 1;

    private void OnEnable()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        Reward += _additionalReward;
        StartHealth += _additionalHealthForBig;
    }

    public override void Hit(int damage)
    {
        if(IsAlive)
            _particle.Play();

        base.Hit(damage);
    }
}
