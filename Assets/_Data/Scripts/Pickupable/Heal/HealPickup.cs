using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : NhoxBehaviour
{
    public int healRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponentInChildren<Damageable>();

        if (damageable)
        {
            bool wasHeal = damageable.Heal(healRestore);
            if (wasHeal) 
                Destroy(gameObject);
        }
    }

    private void Update()
    {
        ItemSpin();
    }

    private void ItemSpin()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
