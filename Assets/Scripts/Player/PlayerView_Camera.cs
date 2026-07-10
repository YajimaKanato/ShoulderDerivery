using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerView
{
    [Header("カメラの操作関連のパラメータ")]
    [Header("カメラオブジェクト"), SerializeField] Transform _cameraTransform;
    [Header("横方向の首振り制限"), SerializeField] float _shakeMaxRange = 45;
    [Header("縦方向の首振り制限"), SerializeField] float _nodMaxRange = 30;
    [Header("マウス感度"), Tooltip("1ピクセルでどれくらい回転させるか"), SerializeField]
    float _mouseSensitivity = 0.01f;
    [Header("コントローラー感度"), Tooltip("基準の何倍の速さで最大角度に到達するか"), SerializeField]
    float _controllerSensitivity = 1f;
    float _yawDelta;
    float _pitchDelta;

    void ShakeAndNod()
    {
        var look = _camera.ReadValue<Vector2>();

        var useGamepad = Gamepad.current != null
            && Gamepad.current.rightStick.ReadValue().sqrMagnitude > 0.01;

        // 横方向の首振り角度
        if (useGamepad)
        {
            _yawDelta += look.x * Time.deltaTime * _controllerSensitivity * _shakeMaxRange;
        }
        else
        {
            _yawDelta += look.x * _shakeMaxRange * _mouseSensitivity;
        }
        _yawDelta = Mathf.Clamp(_yawDelta, -_shakeMaxRange, _shakeMaxRange);

        // 縦方向の首振り角度
        if (useGamepad)
        {
            _pitchDelta += look.y * Time.deltaTime * _controllerSensitivity * _nodMaxRange;
        }
        else
        {
            _pitchDelta += look.y * _nodMaxRange * _mouseSensitivity;
        }
        _pitchDelta = Mathf.Clamp(_pitchDelta, -_nodMaxRange, _nodMaxRange);

        _cameraTransform.localRotation =
            Quaternion.Euler(
                -_pitchDelta
                , _yawDelta
                , 0);
    }
}
