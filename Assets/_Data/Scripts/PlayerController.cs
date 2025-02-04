using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : NhoxBehaviour
{
    public float walkSpeed = 5f;
    public Vector2 moveInput;
    public bool IsMoving;

    [SerializeField] protected Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadRigidbody2d();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        IsMoving = moveInput != Vector2.zero;
    }

    protected void LoadRigidbody2d()
    {
        if (this.rb != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        Debug.Log(this.name + "Load Rigidbody2D", gameObject);
    }
}
