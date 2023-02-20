using System.Collections;
using UnityEngine;


public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _centerRotationFrom;
    [SerializeField] private Power _power;

    private bool _lookFollow = false;
    private float _timeLookAt = 3;

    private void Update()
    {
        if(_lookFollow) 
        {
            gameObject.transform.LookAt(_centerRotationFrom.transform.position);
        }
    }

    private void Rotation()
    {
        _lookFollow = true;
        StartCoroutine(DelayToRotate());
    }

    private IEnumerator DelayToRotate()
    {
        yield return new WaitForSeconds(_timeLookAt);
        _lookFollow = false;
    }

    private void OnEnable()
    {
        _power.PowerUp += Rotation;
    }

    private void OnDisable()
    {
        _power.PowerUp -= Rotation;
    }
}
