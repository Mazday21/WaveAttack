using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _centerRotationTo;
    private bool dol = true;
    private void Update()
    {
        if (dol)
        {
            gameObject.transform.DOLookAt(_centerRotationTo.transform.position, 2);
            dol = false;
        }
    }
}
