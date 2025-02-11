using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : NhoxBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;

    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected DetectionZone attackZone;

    public event System.Action<bool> OnTargetDetected;

    public enum WalkableDirection { Right, Left };

    [SerializeField] protected Vector2 walkDirectionVector = Vector2.right;

    [SerializeField] protected bool hasTarget;
    public bool HasTarget
    {
        get => hasTarget;
        private set
        {
            hasTarget = value;
            OnTargetDetected?.Invoke(hasTarget);
        }
    }

    [SerializeField] protected WalkableDirection walkDirection;

    public WalkableDirection WalkDirection
    {
        get { return walkDirection; }

        set
        {
            if(walkDirection != value)
            {
                gameObject.transform.parent.localScale = new Vector2(gameObject.transform.parent.localScale.x * -1, gameObject.transform.parent.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }

                walkDirection = value;
            }
        }
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        this.Flip();
        this.Move();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2d();
        this.LoadAttackZone();
    }

    protected virtual void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + "Load Rigidbody2D", gameObject);
    }

    protected virtual void LoadAttackZone()
    {
        if (this.attackZone != null) return;
        this.attackZone = transform.parent.GetComponentInChildren<DetectionZone>();
        Debug.Log(transform.name + "Load AttackZone", gameObject);
    }

    protected virtual void Move()
    {
        if (EnemyCtrl.Instance.KnightAnimator.CanMove())
            rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
        else
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
    }

    protected virtual void FlipDirections()
    {
        if(WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.Log(transform.name + " Walk direction not set to Legal value");
        }
    }

    protected virtual void Flip()
    {
        if (EnemyCtrl.Instance.TouchingDirections.IsOnWall && EnemyCtrl.Instance.TouchingDirections.IsGrounded)
        {
            FlipDirections();
        }
    }
}
