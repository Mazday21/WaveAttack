using DG.Tweening;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private GameObject _center;
    [SerializeField] private float _speed;
    [SerializeField] private Power _power;

    private Vector3 _startPosition;
    private Camera _camera;

    private void Start()
    {
        _startPosition = transform.position;
        _camera = GetComponent<Camera>();
        _power = GetComponent<Power>();
    }

    private void Update()
    {
        
    }

    private void Rotate()
    {
        //transform.LookAt(_center.transform);
        //transform.Translate(Vector3.left * Time.deltaTime * _speed);
        _camera.DOShakePosition(1);
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
