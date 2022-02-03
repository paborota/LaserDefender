using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int startingHealth = 50;
    private int _currentHealth;

    // Uncomment _isAlive lines if planning on keeping game object around after death
    //private bool _isAlive;

    private void Start()
    {
        _currentHealth = startingHealth;
        // _isAlive = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.GetComponent<DamageDealer>();
        if (damageDealer == null) return;
        
        TakeDamage(damageDealer.GetDamage());
    }

    public void TakeDamage(int damage)
    {
        // if (!_isAlive) return;
        
        _currentHealth -= damage;
        Mathf.Clamp(_currentHealth, 0.0f, startingHealth);
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // _isAlive = false;
        Destroy(gameObject);
    }
}
