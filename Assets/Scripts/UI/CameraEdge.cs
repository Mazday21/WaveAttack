using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdge : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private Camera _camera;

    private Vector3[] _cameraAngles = new Vector3[4];

    private void OnDrawGizmos()
    {
        //DownLeft
        _cameraAngles[0] = _camera.ScreenToWorldPoint(new Vector3(0f, 0f, _distance));
        //DownRight
        _cameraAngles[1] = _camera.ScreenToWorldPoint(new Vector3(0f, _camera.pixelHeight, _distance));
        //UpRight
        _cameraAngles[2] = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, _camera.pixelHeight, _distance));
        //UpLeft
        _cameraAngles[3] = _camera.ScreenToWorldPoint(new Vector3(_camera.pixelWidth, 0f, _distance));
        Gizmos.DrawLine(_cameraAngles[0], _cameraAngles[1]);
        Gizmos.DrawLine(_cameraAngles[1], _cameraAngles[2]);
        Gizmos.DrawLine(_cameraAngles[2], _cameraAngles[3]);
        Gizmos.DrawLine(_cameraAngles[3], _cameraAngles[0]);
    }
}
