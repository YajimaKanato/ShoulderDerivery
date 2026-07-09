using System;
using UnityEngine;

[Serializable]
public class PlayerMoveParam
{
    [Header("パラメータの変化"), SerializeField] AnimationCurve _speedCurve;
    [Header("最大入力時に最大パラメータに到達するまでの時間"), SerializeField] float _maxSpeedDuration = 1;
    [Header("入力がないときの減衰率"), SerializeField] float _damping = 2;
    const float INPUT_VALID_RANGE = 0.01f;

    public float CalculateDelta(float input, ref float delta)
    {
        if (input != 0 && delta < 1)
        {
            // 入力量に対する最大速度でないなら
            // 入力方向に数値を増やす
            delta += Time.deltaTime / _maxSpeedDuration;
        }
        else
        {
            // 入力がない
            if (delta < INPUT_VALID_RANGE)
            {
                // 一定量を下回ったら0にする
                delta = 0;
            }
            else
            {
                // 入力状態に応じて減少
                delta -= Time.deltaTime / _maxSpeedDuration * (input == 0 ? _damping : 1);
            }
        }

        return _speedCurve.Evaluate(delta);
    }
}
