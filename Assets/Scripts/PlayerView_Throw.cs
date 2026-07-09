using UnityEngine;

public partial class PlayerView
{
    [Header("投擲関連のパラメータ")]
    [Header("投擲力"), SerializeField] float _throwingPower = 10;
    [Header("投擲物"), SerializeField] GameObject _cardboard;
    [Header("投擲位置"), SerializeField] Transform _throwPos;

    void Throw()
    {
        if (_throw.WasPressedThisFrame())
        {
            var go = Instantiate(_cardboard, _throwPos.position, Quaternion.identity);
            if (go.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.AddForce(_cameraTransform.forward * _throwingPower, ForceMode.Impulse);
            }
        }
    }
}
