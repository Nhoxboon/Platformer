using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : NhoxBehaviour
{
    public GameObject projectilePrefab;

    public virtual void FireProjectile()
    {
        Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadProjectilePrefab();
    }

    protected virtual void LoadProjectilePrefab()
    {
        if (projectilePrefab != null) return;
        projectilePrefab = Resources.Load<GameObject>("Arrow");
        Debug.Log(transform.name + " LoadProjectilePrefab", gameObject);
    }

    
}
