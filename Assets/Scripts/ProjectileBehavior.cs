using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // This script is created to be mainly a health manager specifically for projectiles
    // can break off and make more of this.

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Projectile")) return;
        
        Destroy(gameObject);
    }
}
