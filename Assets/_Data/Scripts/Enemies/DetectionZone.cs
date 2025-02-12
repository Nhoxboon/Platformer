using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : NhoxBehaviour
{
    public UnityEvent noCollidersDetected;
    public List<Collider2D> detectedColliders = new List<Collider2D>();

    [SerializeField] protected Collider2D col;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider2d();
    }

    protected virtual void LoadCollider2d()
    {
        if (this.col != null) return;
        this.col = GetComponent<Collider2D>();
        Debug.Log(transform.name + "Load Collider2D", gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision);
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);
        if (detectedColliders.Count <= 0)
        {
            noCollidersDetected.Invoke();
        }
    }
}
