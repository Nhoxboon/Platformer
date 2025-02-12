using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : NhoxBehaviour
{
    [SerializeField] protected Collider2D attackCollider;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Vector2 knockBack =  Vector2.zero;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (attackCollider != null) return;
        attackCollider = GetComponent<Collider2D>();
        Debug.Log(transform.name + " LoadCollider", gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponentInChildren<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockBack = transform.parent.localScale.x > 0 ? knockBack : new Vector2(knockBack.x * -1, knockBack.y);
            _ = damageable.Hit(damage, deliveredKnockBack);
        }
    }
}
