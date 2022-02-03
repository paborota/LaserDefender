using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShootingBehavior : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10.0f;
    [SerializeField] private float projectileLifetime = 5.0f;
    [SerializeField] private float timeBetweenShots = 0.5f;

    [SerializeField] private bool useAI;
    [SerializeField] private float timeBetweenShotsVariance = 0.0f;
    private Vector2 _shootingDirection;

    private Coroutine _firingCoroutine;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        _shootingDirection = transform.up;
        if (!useAI)
        {
            timeBetweenShotsVariance = 0.0f;
            return;
        }
        
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
            _audioPlayer.PlayShootingClip();
            
            var instance = Instantiate(projectilePrefab, gameObject.transform.position, Quaternion.identity);
        
            // @TODO Set projectile instance speed
            var rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity =  _shootingDirection * projectileSpeed;
            }
            // Destroy after set lifetime
            Destroy(instance, projectileLifetime);

            var timeToWait = Random.Range(timeBetweenShots - timeBetweenShotsVariance,
                timeBetweenShots + timeBetweenShotsVariance);
            yield return new WaitForSecondsRealtime(timeToWait);
        }
    }
}
