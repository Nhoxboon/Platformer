using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NhoxBehaviour
{
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float runSpeed = 8f;
    [SerializeField] protected float currentSpeed;
    public Vector2 moveInput;

    [SerializeField] protected bool isFacingRight = true;

    [SerializeField] protected bool isMoving = false;
    public bool IsMoving => isMoving;

    [SerializeField] protected bool isRunning = false;
    public bool IsRunning => isRunning;

    [SerializeField] protected Rigidbody2D rb;

    private void FixedUpdate()
    {
        this.Move();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2d();
    }

    protected virtual void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(transform.name + "Load Rigidbody2D", gameObject);
    }

    

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);

        PlayerCtrl.Instance.PlayerAnimator?.UpdateMovement(isMoving);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            isRunning = true;
            PlayerCtrl.Instance.PlayerAnimator?.UpdateRunning(isRunning);
        }
        else if(context.canceled)
        {
            isRunning = false;
            PlayerCtrl.Instance.PlayerAnimator?.UpdateRunning(isRunning);
        }
    }

    protected void Move()
    {
        currentSpeed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(moveInput.x * currentSpeed, rb.velocity.y);
    }

    protected void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

     void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 currentScale = transform.parent.localScale;
        currentScale.x *= -1;
        transform.parent.localScale = currentScale;
    }

}
