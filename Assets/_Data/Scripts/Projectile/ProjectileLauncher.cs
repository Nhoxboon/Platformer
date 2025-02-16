using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : NhoxBehaviour
{

    public virtual void FireProjectile()
    {
        Transform projectileTransform = ArrowSpawner.Instance.Spawn(ArrowSpawner.arrow, transform.position);

        float direction = Mathf.Sign(transform.parent.parent.localScale.x);
        projectileTransform.localScale = new Vector3(Mathf.Abs(projectileTransform.localScale.x) * direction, projectileTransform.localScale.y, projectileTransform.localScale.z);

        projectileTransform.GetComponentInChildren<Projectile>().Launch();
    }
}
