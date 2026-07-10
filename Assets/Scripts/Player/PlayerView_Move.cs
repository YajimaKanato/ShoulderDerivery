using UnityEngine;

public partial class PlayerView
{
    [Header("移動に関するデータ")]
    [Header("前輪"), SerializeField] WheelCollider _frontWheel;
    [Header("前輪の最大回転角度"), SerializeField] float _maxSteerAngle = 45;
    [SerializeField] PlayerMoveParam _steerParam;
    [Header("後輪"), SerializeField] WheelCollider _realWheel;
    [Header("後輪にかかるモーターの強さ"), SerializeField] float _motorPower = 1000;
    [SerializeField] PlayerMoveParam _moveParam;
    [Header("車体に関するデータ")]
    [Header("車体の傾き角度"), SerializeField] float _maxVehicleAngle = 30;
    [Header("ハンドルオブジェクト"), SerializeField] Transform _handleTransform;
    [Header("車体の傾きの中心"), SerializeField] Transform _pivot;
    float _accelDelta;
    float _brakeDelta;
    float _rightSteerDelta;
    float _leftSteerDelta;

    void Move()
    {
        var accel = _accel.IsPressed() ? 1 : 0;
        var brake = _brake.IsPressed() ? 1 : 0;
        var speed = GetVelocity(accel, brake);
        _realWheel.motorTorque = speed * _motorPower;
    }

    float GetVelocity(float accel, float brake)
    {
        return _moveParam.CalculateDelta(accel, ref _accelDelta) - _moveParam.CalculateDelta(brake, ref _brakeDelta);
    }

    void Steer()
    {
        var steer = _steer.ReadValue<float>();
        var steerAngle = GetSteerAngle(Mathf.Clamp01(steer), Mathf.Clamp01(-steer));
        _frontWheel.steerAngle = steerAngle * _maxSteerAngle;
        _handleTransform.localEulerAngles = new Vector3(_handleTransform.eulerAngles.x, steerAngle * _maxSteerAngle, 0);
        _pivot.localEulerAngles = new Vector3(0, steerAngle * _maxVehicleAngle, 0);
    }

    float GetSteerAngle(float right, float left)
    {
        return _steerParam.CalculateDelta(right, ref _rightSteerDelta) - _steerParam.CalculateDelta(left, ref _leftSteerDelta);
    }
}
