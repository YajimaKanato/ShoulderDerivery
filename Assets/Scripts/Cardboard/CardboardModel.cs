using UnityEngine;

[CreateAssetMenu(fileName = "CardboardModel", menuName = "Model/CardboardModel")]
public class CardboardModel : ScriptableObject
{
    [SerializeField] int _score;
    [SerializeField] int _weight;

    public int Score => _score;
    public int Weight => _weight;
}
