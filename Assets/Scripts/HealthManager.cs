using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public delegate void PlayerHealthUpdated();
    public event PlayerHealthUpdated OnPlayerHealthUpdated;
    
    public delegate void PlayerDied();
    public event PlayerDied OnPlayerDeath;

    [Header("Player Params")]
    [Tooltip("AI Params ignored if set to true.")]
    [SerializeField] private bool isPlayer;
    [SerializeField] private int startingHealth = 50;

    public int StartingHealth => startingHealth;

    public int CurrentHealth { get; private set; }


    [Header("Enemy Params")]
    [Tooltip("Points added to game score when this unit is destroyed by player.")]
    [SerializeField] private int scoreForKill;

    [Header("Effects")]
    [SerializeField] private ParticleSystem explosionEffect;
    
    [Header("Needed GameObjects")]
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
        
        // Player Only Setup
        if (!isPlayer) return;
        
        var levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            OnPlayerDeath += levelManager.GameOver;
        }

        if (Camera.main != null)
        {
            _cameraShake = Camera.main.GetComponent<CameraShake>();
        }
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
        CurrentHealth = Math.Clamp(CurrentHealth, 0, startingHealth);
        
        if (isPlayer)
        {
            OnPlayerHealthUpdated?.Invoke();
            Debug.Log("Current Health: " + CurrentHealth);
        }
        
        if (CurrentHealth <= 0)
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
        switch (isPlayer)
        {
            case false when _scoreKeeper != null:
                _scoreKeeper.ModifyScore(ref scoreForKill);
                break;
            case true:
                OnPlayerDeath?.Invoke();
                break;
        }
        
        Destroy(gameObject);
    }
}
