using System.Collections.Generic;
using UnityEngine;

public class CardboardPool : MonoBehaviour
{
    [SerializeField] Cardboard _cardboardPrefab;
    Queue<Cardboard> _pool = new();
    // 重さを取得
    public Cardboard Prefab => _cardboardPrefab;

    public Cardboard GetCardboard()
    {
        Cardboard cardboard = null;
        if (_pool.Count > 0)
        {
            cardboard = _pool.Dequeue();
            cardboard.gameObject.SetActive(true);
        }
        else
        {
            cardboard = Instantiate(_cardboardPrefab);
            cardboard.Init(this);
        }
        return cardboard;
    }

    public void ReleaseToPool(Cardboard cardboard)
    {
        _pool.Enqueue(cardboard);
        cardboard.gameObject.SetActive(false);
    }
}
