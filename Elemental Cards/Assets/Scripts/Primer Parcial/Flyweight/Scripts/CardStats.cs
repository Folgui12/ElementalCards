using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Stats", order = 0)]
public class CardStats : ScriptableObject
{
    [SerializeField] private string _cardName;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private int _damage;
    [SerializeField] private int _protection;
    [SerializeField] private ElementalPower _element;

    public string CardName => _cardName;
    public string Description => _description;
    public int Cost => _cost;
    public int Damage => _damage;
    public int Protection => _protection;
    public ElementalPower Element => _element;

}
