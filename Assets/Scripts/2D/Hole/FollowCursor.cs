using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, transform.position.y, _startPosition.z);
    }
}
