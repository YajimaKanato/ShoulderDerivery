using UnityEngine;

public partial class PlayerView
{
    [Header("移動に関するデータ")]
    [Header("前輪の最大回転角度"), SerializeField] float _maxSteerAngle = 45;
    [Header("止まってるときの旋回角度"), SerializeField] float _stopTurnAngle = 45;
    [Header("止まってるときの旋回速度"), SerializeField] float _stopTurnSpeed = 4;
    [SerializeField] PlayerMoveParam _steerParam;
    [Header("後輪にかかるモーターの強さ"), SerializeField] float _motorPower = 1000;
    [SerializeField] PlayerMoveParam _moveParam;
    [Header("車体に関するデータ")]
    [Header("車体の傾き角度"), SerializeField] float _maxVehicleAngle = 30;
    [Header("車体のオブジェクト"), SerializeField] Transform _vehicle;
    [Header("ハンドルオブジェクト"), SerializeField] Transform _handleTransform;
    [Header("車体の傾きの中心"), SerializeField] Transform _pivot;
    float _accelDelta;
    float _brakeDelta;
    float _rightSteerDelta;
    float _leftSteerDelta;

    void CalculateDirection()
    {
        var steer = Steer();
        var speed = Move();
        Quaternion newRot;
        Vector3 newPos;
        if (speed == 0 && steer != 0)
        {
            // その場で旋回したいとき
            var rot = Quaternion.AngleAxis(steer * _stopTurnAngle * Time.fixedDeltaTime, Vector3.up);
            newRot = rot * _rb.rotation;
            newPos = _rb.position + transform.forward * _stopTurnSpeed * Time.fixedDeltaTime;
        }
        else
        {
            // 止まっていない
            var rot = Quaternion.AngleAxis(steer * _maxSteerAngle * Time.fixedDeltaTime * Mathf.Sign(speed), Vector3.up);
            newRot = rot * _rb.rotation;
            newPos = _rb.position + transform.forward * speed * _motorPower * Time.fixedDeltaTime;
        }
        _rb.MovePosition(newPos);
        _rb.MoveRotation(newRot);

        // 曲がってる感じを出す
        _vehicle.localEulerAngles = new Vector3(0, 0, -steer * _maxVehicleAngle * speed);
        var handle = _handleTransform.localEulerAngles;
        handle.y = steer * _maxSteerAngle * speed;
        _handleTransform.localEulerAngles = handle;
        _pivot.localEulerAngles = new Vector3(0, steer * _maxVehicleAngle * speed, -steer * _maxVehicleAngle * speed);
    }

    /// <summary>
    /// 速度量を計算するメソッド
    /// </summary>
    /// <returns>-1~1でどれくらいの速度かを返す</returns>
    float Move()
    {
        var accel = _accel.IsPressed() ? 1 : 0;
        var brake = _brake.IsPressed() ? 1 : 0;
        var speed = _moveParam.CalculateDelta(accel, ref _accelDelta) - _moveParam.CalculateDelta(brake, ref _brakeDelta);
        return speed;
    }

    /// <summary>
    /// ステアリング角度を計算するメソッド
    /// </summary>
    /// <returns>-1~1でどの角度に傾いているかを返す</returns>
    float Steer()
    {
        // ステアリング角度計算
        var steer = _steer.ReadValue<float>();
        var steerAngle = _steerParam.CalculateDelta(Mathf.Max(0, steer), ref _rightSteerDelta) - _steerParam.CalculateDelta(Mathf.Min(0, steer), ref _leftSteerDelta);
        return steerAngle;
    }
}
