using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : NhoxBehaviour
{
    public GameObject projectilePrefab;

    public virtual void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;

        projectile.transform.localScale = new Vector3(origScale.x * transform.parent.localScale.x > 0 ? 1 : -1, origScale.y, origScale.z);
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
