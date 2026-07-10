using UnityEngine;

public partial class PlayerView : MonoBehaviour
{

    void Init()
    {
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
        Move();
        Steer();
        ShakeAndNod();
        Throw();
        LockOn();
    }

    private void FixedUpdate()
    {
    }
}
