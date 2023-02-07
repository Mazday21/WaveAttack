using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor2 : MonoBehaviour
{
    private Vector3 pos;
    private Vector3 imagePosition;
    private Vector3 cursorPosition;
    public float offset = 6f;

    void Start()
    {
        imagePosition = transform.position;
    }

    private void Update()
    {
        pos = Input.mousePosition;
        pos.z = offset;
        //transform.position = Camera.main.ScreenToWorldPoint(pos);
        Vector3 XPosition = Camera.main.ScreenToWorldPoint(pos);
        transform.position = new Vector3(imagePosition.x, imagePosition.y, XPosition.z);
    }
}
