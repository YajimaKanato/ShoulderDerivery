using UnityEngine;

public partial class PlayerView : MonoBehaviour
{

    void Init()
    {
        CacheAction();
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
    }

    private void FixedUpdate()
    {
    }
}
