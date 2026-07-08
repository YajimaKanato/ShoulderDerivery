using UnityEngine.InputSystem;

public partial class PlayerView
{
    Actions _actions;
    InputAction _move;

    void CacheAction(Actions actions)
    {
        _actions = actions;
        _move = _actions.Ingame.Move;
    }
}
