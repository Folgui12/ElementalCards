using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Actor : MonoBehaviour, IDamageable
{
    public ActorStats Stats => _stats;

    [SerializeField] protected ActorStats _stats;
    protected Slider _showCurrentLife;


    public virtual void Attack() { }
    
    public virtual void TakeDamage(float damage, ElementalPower elemento = null)
    {
        _stats.CurrentLife -= damage;

        UpdateLifeBar();

        if (_stats.CurrentLife <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void UpdateLifeBar()
    {
        _showCurrentLife.value = _stats.CurrentLife;
    }

    public void Heal(float life)
    {
        _stats.CurrentLife += life * .8f;

        if (_stats.CurrentLife > _stats.MaxLife)
            _stats.CurrentLife = _stats.MaxLife;

        UpdateLifeBar();
    }
}
