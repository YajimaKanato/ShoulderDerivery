using UnityEngine.InputSystem;

public partial class PlayerView
{
    Actions _actions;
    InputAction _accel;
    InputAction _brake;
    InputAction _steer;

    void CacheAction(Actions actions = null)
    {
        _actions = actions ?? new();
        _actions.Enable();
        _accel = _actions.Ingame.Accel;
        _brake = _actions.Ingame.Brake;
        _steer = _actions.Ingame.Steer;
    }
}
