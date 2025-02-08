using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : NhoxBehaviour
{
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float cellingDistance = 0.05f;

    [SerializeField] protected CapsuleCollider2D touchingCol;

    public event Action<bool> OnGroundedChanged;
    public event Action<bool> OnWallChanged;
    public event Action<bool> OnCellingChanged;


    [SerializeField] protected RaycastHit2D[] groundHits = new RaycastHit2D[5];
    [SerializeField] protected RaycastHit2D[] wallHits = new RaycastHit2D[5];
    [SerializeField] protected RaycastHit2D[] cellingHits = new RaycastHit2D[5];


    [SerializeField] protected bool isGrounded;
    public bool IsGrounded
    {
        get {
            return isGrounded;
               }
        private set
        {
            isGrounded = value;
            OnGroundedChanged?.Invoke(value);
        }
    }

    [SerializeField] protected bool isOnWall;
    public bool IsOnWall
    {
        get
        {
            return isOnWall;
        }
        private set
        {
            isOnWall = value;
            OnWallChanged?.Invoke(value);
        }
    }

    [SerializeField] protected bool isOnCelling;
    public bool IsOnCelling
    {
        get
        {
            return isOnCelling;
        }
        private set
        {
            isOnCelling = value;
            OnCellingChanged?.Invoke(value);
        }
    }

    protected Vector2 wallCheckDirection => gameObject.transform.parent.localScale.x > 0 ? Vector2.right : Vector2.left;

    private void FixedUpdate()
    {
        this.CheckGrounded();

        this.CheckOnWall();

        this.CheckOnCelling();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider2D();
    }

    protected virtual void LoadCollider2D()
    {
        if (this.touchingCol != null) return;
        this.touchingCol = GetComponentInParent<CapsuleCollider2D>();
        Debug.Log(transform.name + "Load Collider2D", gameObject);
    }

    protected virtual void CheckGrounded()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
    }

    protected virtual void CheckOnWall()
    {
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
    }

    protected virtual void CheckOnCelling()
    {
        IsOnCelling = touchingCol.Cast(Vector2.up, castFilter, cellingHits, cellingDistance) > 0;
    }
}
