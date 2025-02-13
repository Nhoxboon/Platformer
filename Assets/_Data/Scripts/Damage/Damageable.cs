using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : NhoxBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    [SerializeField] protected int maxHealth = 100;
    public int MaxHealth => maxHealth;

    [SerializeField] protected int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;

            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    [SerializeField] protected float timeSinceHit = 0;
    [SerializeField] protected float invincibleTime = 0.25f;
    [SerializeField] protected bool isInvincible = false;
    
    public event Action<bool> OnAliveStateChanged;
    public event Action<bool> OnHitStateChanged;
    public Func<bool> GetIsHitFromAnimator;

    [SerializeField] protected bool isAlive = true;
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            OnAliveStateChanged?.Invoke(isAlive);
        }
    }

    public bool IsHit 
    { 
        get => GetIsHitFromAnimator?.Invoke() ?? false;
        set
        {
            OnHitStateChanged?.Invoke(value);
        }
    }

    private void Update()
    {
        this.Invincible();
    }

    public bool Hit(int damage, Vector2 knockBack)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            IsHit = true;
            damageableHit?.Invoke(damage, knockBack);

            CharacterEvents.characterDamaged?.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }

    public bool Heal(int heal)
    {
        if (IsAlive && health < maxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, heal);
            Health += actualHeal;
            CharacterEvents.characterHealed?.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }

    protected virtual void Invincible()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }
    public override void Reset()
    {
        base.Reset();
        Health = maxHealth;
        IsAlive = true;
    }

}
