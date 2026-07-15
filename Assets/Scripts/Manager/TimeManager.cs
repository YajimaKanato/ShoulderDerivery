using TMPro;
using UnityEngine;

public class TimeManager : ManagerBase
{
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] float _timeLimit = 60;
    float _delta;

    public override void Init(IngameManager ingameManager)
    {
        base.Init(ingameManager);
        _delta = _timeLimit;
    }

    private void Update()
    {
        if (_gameEnd) return;
        _delta -= Time.deltaTime;
        if (_delta <= 0)
        {
            _delta = 0;
            _ingameManager.TimeUp();
        }
        _timerText.text = _delta.ToString("0.0");
    }

    public override void EndGame()
    {
        _gameEnd = true;
    }
}
