using UnityEngine;

public partial class PlayerView
{
    [Header("投擲関連のパラメータ")]
    [Header("投擲力"), SerializeField] float _throwingPower = 10;
    [Header("投擲物"), SerializeField] GameObject _cardboard;
    [Header("投擲開始位置"), SerializeField] Transform _throwPos;
    [Header("投擲の予測線"), SerializeField] GuideLineRenderer _guideLineRenderer;
    [Header("予測線の長さ"), SerializeField] int _guideLineLength = 50;
    [Header("段ボールのオブジェクトプール"), SerializeField] CardboardPool _pool;

    void ThrowInit()
    {

    }

    void Throw()
    {
        if (_throw.WasPressedThisFrame())
        {
            var go = _pool?.GetCardboard();
            if (go.TryGetComponent<Rigidbody>(out var rb))
            {
                go.Ready(_throwPos.position);
                rb.AddForce(_throwPos.forward * _throwingPower, ForceMode.Impulse);
            }
        }
    }

    void LockOn()
    {
        if (_lockOn.IsPressed())
        {
            // 軌道予測
            var position = _throwPos.position;
            var velocity = _throwPos.forward * _throwingPower / _pool.Prefab.GetComponent<Rigidbody>().mass;
            var dt = Time.fixedDeltaTime;
            var points = new Vector3[_guideLineLength];
            for (int i = 0; i < _guideLineLength; i++)
            {
                velocity += Physics.gravity * dt;
                position += velocity * dt;

                points[i] = position;
            }
            _guideLineRenderer.SetPoints(points);
        }
        else if (_lockOn.WasReleasedThisFrame())
        {
            _guideLineRenderer.DisableMesh();
        }
    }
}
