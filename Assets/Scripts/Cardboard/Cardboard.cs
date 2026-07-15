using UnityEngine;

public class Cardboard : MonoBehaviour
{
    [SerializeField] CardboardModel _cardboardModel;
    [Header("生存時間"), SerializeField] float _lifeTime = 5f;
    Rigidbody _rb;
    TrailRenderer _trailRender;
    CardboardPool _parentPool;
    float _delta;
    bool _isReady;

    public void Init(CardboardPool pool)
    {
        _parentPool = pool;
        _rb = GetComponent<Rigidbody>();
        _trailRender = GetComponent<TrailRenderer>();
    }

    public void Ready(Vector3 pos)
    {
        _delta = 0;
        _rb.linearVelocity = Vector3.zero;
        _isReady = true;
        _trailRender.emitting = false;
        transform.position = pos;
        _trailRender.Clear();
        _trailRender.emitting = true;
    }

    private void Update()
    {
        if (!_isReady) return;
        _delta += Time.deltaTime;
        if (_lifeTime <= _delta)
        {
            ReleaseToPool();
        }
    }

    public void ReleaseToPool()
    {
        _isReady = false;
        _parentPool.ReleaseToPool(this);
    }
}
