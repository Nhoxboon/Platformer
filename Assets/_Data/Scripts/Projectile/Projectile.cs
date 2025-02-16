using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : NhoxBehaviour
{
    [SerializeField] protected int damage = 10;
    [SerializeField] protected Vector2 moveSpeed = new Vector2(8f, 0);
    [SerializeField] protected Vector2 knockBack = new Vector2(3f, 0);

    [SerializeField] protected Rigidbody2D rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2d();
    }

    protected virtual void LoadRigidbody2d()
    {
        if (rb != null) return;
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + " Load rigidbody2d", gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponentInChildren<Damageable>();

        if(damageable != null)
        {
            Vector2 deliveredKnockBack = transform.localScale.x > 0 ? knockBack : new Vector2(knockBack.x * -1, knockBack.y);
            _ = damageable.Hit(10, deliveredKnockBack);
            ArrowSpawner.Instance.Despawn(transform);
        }

        ArrowSpawner.Instance.Despawn(transform);
    }

    public void Launch()
    {
        rb.velocity = new Vector2(moveSpeed.x * Mathf.Sign(transform.localScale.x), moveSpeed.y);
    }
}
