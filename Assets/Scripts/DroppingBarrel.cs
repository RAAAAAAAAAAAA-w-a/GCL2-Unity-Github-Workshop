using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroppingBarrel : Barrel
{

    protected Collider2D collider;

    protected override void Start()
    {
        base.Start();
        collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("BarrelDrop"))
        {
            collider.isTrigger = true;
        }
        rb.linearVelocity = Vector2.zero;
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground"))
            return;
        collider.isTrigger = false;
    }

}