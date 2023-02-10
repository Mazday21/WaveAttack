using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private float offsetZ = 6f;

    private Vector3 _position;
    private Vector3 _imagePosition;
    private float _offsetY = 80;

    public float OffsetZ { get; private set; }

    void Start()
    {
        _imagePosition.y = transform.position.y;
        OffsetZ = offsetZ;
    }

    private void Update()
    {
        _position = Input.mousePosition;
        _position.z = offsetZ;
        _position.y = _imagePosition.y + _offsetY;
        transform.position = Camera.main.ScreenToWorldPoint(_position);
    }
}
