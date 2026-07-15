using UnityEngine;

public class DeriveryManager : MonoBehaviour
{
    [SerializeField] int _deriveryCount = 5;
    Target[] _targets;
    int _currentDeriveryIndex = -1;
    int _currentDeriveryCount = 0;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        _targets = FindObjectsByType<Target>(FindObjectsSortMode.None);
        Shuffle();
        NextDerivery();
    }

    void Shuffle()
    {
        for (int i = 0; i < _targets.Length; i++)
        {
            var rand = Random.Range(0, _targets.Length - 1);
            (_targets[i], _targets[rand]) = (_targets[rand], _targets[i]);
        }

        foreach (var target in _targets)
        {
            target.Init(this);
        }
    }
            

    public void NextDerivery()
    {
        if (_currentDeriveryCount >= _deriveryCount) return;
        _currentDeriveryCount++;
        _currentDeriveryIndex++;
        _currentDeriveryIndex %= _deriveryCount;
        _targets[_currentDeriveryIndex].RequestDerivery();
    }
}
