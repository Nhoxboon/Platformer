using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : NhoxBehaviour
{
    public int healRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    public AudioSource pickupSource;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPickupAudio();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponentInChildren<Damageable>();

        if (damageable)
        {
            bool wasHeal = damageable.Heal(healRestore);
            if (wasHeal)
            {
                AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                Destroy(gameObject);
            }
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

    protected void LoadPickupAudio()
    {
        if (pickupSource != null) return;
        pickupSource = GetComponent<AudioSource>();
        Debug.Log(transform.name + " Load Pickup Audio", gameObject);
    }
}
