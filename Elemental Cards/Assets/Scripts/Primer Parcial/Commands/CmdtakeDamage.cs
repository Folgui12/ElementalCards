using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdtakeDamage : ICommand
{
    private IDamageable _victim;
    private int _damageCause;
    private ElementalPower _element;

    public CmdtakeDamage(IDamageable victim, int damageCause, ElementalPower element = null)
    {
        _victim = victim;
        _damageCause = damageCause;
        _element = element;
    }

    public void Do()
    {
        _victim.TakeDamage(_damageCause, _element);
    }

    public void UnDo()
    {
        Debug.Log("Player Healed");
        _victim.Heal(_damageCause);
    }
}
