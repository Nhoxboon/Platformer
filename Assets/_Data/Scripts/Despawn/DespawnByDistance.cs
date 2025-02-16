using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float disLimit = 50f;
    [SerializeField] protected float distance = 0f;
    [SerializeField] protected Camera mainCam;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCamera();
    }

    protected virtual void LoadCamera()
    {
        if (this.mainCam != null)
        {
            return;
        }
        this.mainCam = Transform.FindObjectOfType<Camera>();
        Debug.Log(transform.parent.name + ": LoadCamera", gameObject);
    }

    protected override bool CanDespawn()
    {
        this.distance = Vector3.Distance(transform.position, this.mainCam.transform.position);
        if (this.distance > this.disLimit)
        {
            return true;
        }
        return false;
    }
}
