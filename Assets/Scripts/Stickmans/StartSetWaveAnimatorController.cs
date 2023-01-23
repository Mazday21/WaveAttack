using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class StartSetWaveAnimatorController : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController _rightFallController;
    [SerializeField] private AnimatorController _leftFallController;

    private void Awake()
    {
        int fiftyPerChance = Random.Range(0, 2);
        Animator animator = gameObject.GetComponent<Animator>();

        if (fiftyPerChance == 0)
        {
            
            animator.runtimeAnimatorController = _rightFallController;
        }
        else
        {
            animator.runtimeAnimatorController = _leftFallController;
        }
    }
}
