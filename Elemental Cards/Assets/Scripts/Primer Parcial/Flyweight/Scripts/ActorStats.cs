using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actor/Stats", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _maxLife;
    [SerializeField] private ElementalPower _power;
    public float CurrentLife;

    public string Name => _name;
    public float MaxLife => _maxLife;
    public ElementalPower Power => _power;
}
