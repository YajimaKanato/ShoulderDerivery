using UnityEngine;

public class TargetPin : MonoBehaviour
{
    Camera _camera;
    bool _lookCameraForward;

    private void Awake()
    {
        _camera = Camera.main;
        _lookCameraForward = true;
    }

    private void Update()
    {
        if (!_lookCameraForward) return;
        transform.forward = _camera.transform.forward;
    }

    public void RestartRotation()
    {
        _lookCameraForward = true;
    }

    public void StopRotation()
    {
        _lookCameraForward = false;
    }
}
