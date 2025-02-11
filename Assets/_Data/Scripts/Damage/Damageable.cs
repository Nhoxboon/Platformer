using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : NhoxBehaviour
{

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

    private void Update()
    {
        this.Invincible();
    }

    public bool Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
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
