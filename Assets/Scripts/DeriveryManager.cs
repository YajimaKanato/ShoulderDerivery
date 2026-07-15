using TMPro;
using UnityEngine;

public class DeriveryManager : MonoBehaviour
{
    [SerializeField] RectTransform _canvas;
    [SerializeField] TextMeshProUGUI _distanceText;
    [SerializeField] float _spaceX = 0.8f;
    [SerializeField] float _spaceY = 0.8f;
    [SerializeField] int _deriveryCount = 5;
    Target[] _targets;
    Camera _camera;
    int _currentDeriveryIndex = -1;
    int _currentDeriveryCount = 0;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpdateDistanceText();
    }

    void Init()
    {
        _targets = FindObjectsByType<Target>(FindObjectsSortMode.None);
        _camera = Camera.main;
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

    void UpdateDistanceText()
    {
        if (_currentDeriveryCount >= _deriveryCount) return;
        var target = _targets[_currentDeriveryIndex].transform.position;
        var viewpoint = _camera.WorldToViewportPoint(target);
        var canvasX = 0f;
        var canvasY = 1f;
        if (0 <= viewpoint.x && viewpoint.x <= 1 && viewpoint.z > 0)
        {
            // 画面内
            canvasX = Mathf.Clamp01(viewpoint.x);
        }
        else
        {
            // 画面外
            var dir = target - _camera.transform.position;
            dir.Normalize();
            canvasX = Vector3.Dot(dir, _camera.transform.right) < 0 ? 0 : 1;
            canvasY = Vector3.Dot(_camera.transform.forward, dir) * 0.5f + 0.5f;
        }
        _distanceText.transform.localPosition = new Vector3((canvasX - 0.5f) * _canvas.rect.width * _spaceX, (canvasY - 0.5f) * _canvas.rect.height * _spaceY, 0);
        _distanceText.text = Vector3.Distance(_camera.transform.position, target).ToString("0000") + " m";
    }
}
