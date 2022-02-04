using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public delegate void PlayerHealthUpdated();
    public event PlayerHealthUpdated OnPlayerHealthUpdated;


    [SerializeField] private bool isPlayer;
    [SerializeField] private int startingHealth = 50;

    public int StartingHealth => startingHealth;

    public int CurrentHealth { get; private set; }


    [SerializeField] private int scoreForKill = 0;

    [SerializeField] private ParticleSystem explosionEffect;
    
    private CameraShake _cameraShake;
    private AudioPlayer _audioPlayer;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        
        CurrentHealth = startingHealth;
        
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (_scoreKeeper == null)
        {
            Debug.Log("Score keeper object could not be found.");
        }

        // Check to make sure we're the player
        LayerMask playerLayerMask = LayerMask.GetMask("Player");
        if (((1 << gameObject.layer) & playerLayerMask) == 0) return;

        // Check to make sure the main camera is not a null reference
        if (Camera.main == null) return;
        _cameraShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        var damageDealer = col.GetComponent<DamageDealer>();
        if (damageDealer == null) return;

        TakeDamage(damageDealer.GetDamage());
    }

    public void TakeDamage(int damage)
    {
        PlayHitEffect();
        PlayCameraShake();
        PlayDamageSound();
        
        CurrentHealth -= damage;
        if (isPlayer)
        {
            OnPlayerHealthUpdated?.Invoke();
        }

        Mathf.Clamp(CurrentHealth, 0.0f, startingHealth);
        if (CurrentHealth == 0)
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
        if (!isPlayer && _scoreKeeper != null)
        {
            _scoreKeeper.ModifyScore(ref scoreForKill);
        }
        
        Destroy(gameObject);
    }
}
