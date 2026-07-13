using UnityEngine;

public partial class PlayerView : MonoBehaviour
{
    Rigidbody _rb;

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        CacheAction();
        ThrowInit();
    }

    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        ShakeAndNod();
        Throw();
        LockOn();
    }

    private void FixedUpdate()
    {
        Move();
        Steer();
        CalculateDirection();
    }
}
