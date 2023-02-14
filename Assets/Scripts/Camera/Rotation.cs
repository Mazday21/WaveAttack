using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Power _power;

    private void Rotate()
    {
        
    }

    private void OnEnable()
    {
        _power.PowerUp += Rotate;
    }

    private void OnDisable()
    {
        _power.PowerUp -= Rotate;
    }
}
