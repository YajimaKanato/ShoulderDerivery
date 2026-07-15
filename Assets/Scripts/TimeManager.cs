using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] float _timeLimit = 60;
    float _delta;

    private void Awake()
    {
        _delta = _timeLimit;
    }

    private void Update()
    {
        _delta -= Time.deltaTime;
        if (_delta <= 0)
        {
            _delta = 0;
        }
        _timerText.text = _delta.ToString("0.0");
    }
}
