using Cysharp.Threading.Tasks;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    [SerializeField] ManagerBase[] _managers;
    bool _isTimeUp;
    bool _deriveryCompleted;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _isTimeUp = false;
        _deriveryCompleted = false;
        foreach (var manager in _managers)
        {
            manager.Init(this);
        }
        EndGameTask().Forget();
    }

    async UniTask EndGameTask()
    {
        await UniTask.WaitUntil(() => _isTimeUp || _deriveryCompleted);
        foreach (var manager in _managers)
        {
            manager.EndGame();
        }
        Debug.Log("ゲーム終了");
    }

    public void TimeUp()
    {
        _isTimeUp = true;
    }

    public void CompleteDerivery()
    {
        _deriveryCompleted = true;
    }
}
