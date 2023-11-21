using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float damage, ElementalPower element = null);
    public void Heal(float life);
    public void Die();
}
