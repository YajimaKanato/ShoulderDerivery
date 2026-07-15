using UnityEngine;

public class Target : MonoBehaviour
{
    TargetPin _targetPin;
    DeriveryManager _manager;
    bool _requested;

    public void Init(DeriveryManager manager)
    {
        _manager = manager;
        _targetPin = GetComponentInChildren<TargetPin>();
        _requested = false;
        _targetPin.gameObject.SetActive(_requested);
    }

    public void RequestDerivery()
    {
        _requested = true;
        _targetPin.gameObject.SetActive(_requested);
        Debug.Log("配達してぇ", gameObject);
    }

    public void CompleteDerivery()
    {
        _requested = false;
        _targetPin.gameObject.SetActive(_requested);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_requested) return;
        if (collision.gameObject.CompareTag("Cardboard")
            && collision.gameObject.TryGetComponent<Cardboard>(out var cardboard))
        {
            cardboard.ReleaseToPool();
            CompleteDerivery();
            Debug.Log("配達ありがとぉ");
            _manager.NextDerivery();
        }
    }
}
