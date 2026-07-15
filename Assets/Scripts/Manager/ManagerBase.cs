using UnityEngine;

public abstract class ManagerBase : MonoBehaviour
{
    protected IngameManager _ingameManager;
    protected bool _gameEnd;

    public virtual void Init(IngameManager ingameManager)
    {
        _ingameManager = ingameManager;
        _gameEnd = false;
    }

    public abstract void EndGame();
}
