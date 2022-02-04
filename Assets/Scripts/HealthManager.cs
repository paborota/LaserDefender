using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // public delegate void PlayerDamaged();
    // public event PlayerDamaged OnPlayerDamaged;

    private ScoreKeeper _scoreKeeper;

        [SerializeField] private int startingHealth = 50;
    private int _currentHealth;

    [SerializeField] private ParticleSystem explosionEffect;
    
    private CameraShake _cameraShake;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        
        _currentHealth = startingHealth;
        
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        
        // if (gameObject.CompareTag("Player") && _scoreKeeper != null)
        // {
        //     _scoreKeeper.IniPlayerHealth(this);
        // }
        
        // Check to make sure we're the player
        LayerMask playerLayerMask = LayerMask.GetMask("Player");
        if (((1 << gameObject.layer) & playerLayerMask) == 0) return;

        // Check to make sure the main camera is not a null reference
        if (Camera.main == null) return;
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        DamageDealer damageDealer = col.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        TakeDamage(damageDealer.GetDamage());
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void TakeDamage(int damage)
    {
        PlayHitEffect();
        PlayCameraShake();
        PlayDamageSound();
        
        _currentHealth -= damage;
        if (gameObject.CompareTag("Player") && _scoreKeeper != null)
        {
            // OnPlayerDamaged?.Invoke();
            _scoreKeeper.OnPlayerDamaged(ref _currentHealth);
        }
        
        Mathf.Clamp(_currentHealth, 0.0f, startingHealth);
        if (_currentHealth == 0)
        {
            Die();
        }
    }

    private void PlayHitEffect()
    {
        if (explosionEffect == null) return;
        
        ParticleSystem instance = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        var main = instance.main;
        Destroy(instance.gameObject, main.duration + main.startLifetime.constantMax);
    }

    private void PlayCameraShake()
    {
        if (_cameraShake == null) return;
        
        _cameraShake.Play();
    }

    private void PlayDamageSound()
    {
        if (_audioPlayer == null) return;
        
        _audioPlayer.PlayDamageClip();
    }

    private void Die()
    {
        if (_scoreKeeper != null)
        {
            _scoreKeeper.OnDeath(gameObject);
        }
        
        Destroy(gameObject);
    }
}
