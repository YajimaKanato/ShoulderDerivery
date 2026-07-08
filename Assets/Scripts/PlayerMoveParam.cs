using System;
using UnityEngine;

[Serializable]
public class PlayerMoveParam
{
    [Header("速度の変化"), SerializeField] AnimationCurve _speedCurve;
    [Header("最大入力時に最大速度に到達するまでの時間"), SerializeField] float _maxSpeedDuration = 1;
    [Header("入力なし時の減衰率"), SerializeField] float _noInputDamping = 2;
    const float INPUT_VALID_RANGE = 0.01f;
    float _delta;

    public float GetVelocity(float input)
    {
        if (input != 0 && Mathf.Abs(_delta) < Mathf.Abs(input))
        {
            // 入力量に対する最大速度でないなら
            // 入力方向に数値を増やす
            _delta += Time.deltaTime / (_maxSpeedDuration * Mathf.Sign(input));
        }
        else
        {
            // 入力がない
            if (Mathf.Abs(_delta) < INPUT_VALID_RANGE)
            {
                // 一定量を下回ったら0にする
                _delta = 0;
            }
            else
            {
                // 入力状態に応じて減少
                _delta -= Time.deltaTime * Mathf.Sign(_delta) * _noInputDamping;
            }
        }

        // カーブにおける数値に変換
        var speed = _speedCurve.Evaluate(_delta);
        return speed;
    }
}
