using UnityEngine;

public partial class PlayerView
{
    [Header("移動に関するデータ")]
    [Header("前輪"), SerializeField] WheelCollider _frontWheel;
    [Header("前輪の最大回転角度"), SerializeField] float _maxSteerAngle = 45;
    [Header("後輪"), SerializeField] WheelCollider _realWheel;
    [Header("後輪にかかるモーターの強さ"), SerializeField] float _motorPower = 1000;
    [SerializeField] PlayerMoveParam _playerParam;


    void Move()
    {
        var dir = _move.ReadValue<Vector2>();


    }
}
