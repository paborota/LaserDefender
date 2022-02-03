using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float projectileLifetime = 5.0f;
    [SerializeField] private float timeBetweenShots = 0.5f;

    [SerializeField] private bool useAI;
    private Vector2 _shootingDirection;

    private Coroutine _firingCoroutine;

    private void Start()
    {
        _shootingDirection = transform.up;
        if (!useAI) return;
        
        _shootingDirection = -_shootingDirection;
        StartFiring();
    }

    public void StartFiring()
    {
        _firingCoroutine = StartCoroutine(FireContinuously());
    }

    public void StopFiring()
    {
        StopCoroutine(_firingCoroutine);
    }

    private IEnumerator FireContinuously()
    {
        // This will need to be terminated outside of the function using StopCoroutine.
        while (true)
        {
            var instance = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        
            // @TODO Set projectile instance speed
            var rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity =  _shootingDirection * projectileSpeed;
            }
            // Destroy after set lifetime
            Destroy(instance, projectileLifetime);

            yield return new WaitForSecondsRealtime(timeBetweenShots);
        }
    }
}
