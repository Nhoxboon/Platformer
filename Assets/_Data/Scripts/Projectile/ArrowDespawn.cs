using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDespawn : DespawnByDistance
{
    public override void DespawnObject()
    {
        ArrowSpawner.Instance.Despawn(transform.parent);
    }
}
