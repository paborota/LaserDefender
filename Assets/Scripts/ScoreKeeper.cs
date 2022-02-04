using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private HealthManager _playerHealthManager;
    private int _playerCurrentHealth;

    private int _currentScore;

    /* SCORE KEEPING METHODS */
    public void OnDeath(GameObject obj)
    {
        if (obj == null) return;
        
        if (obj.CompareTag("Player"))
        {
            _playerCurrentHealth = 0;
        }
        else
        {
            _currentScore += 100;
        }
        
        Debug.Log(_currentScore);
    }
    

    /* PLAYER HEALTH METHODS */
    // public void IniPlayerHealth(HealthManager playerHealthManager)
    // {
    //     _playerHealthManager = playerHealthManager;
    //
    //     SubscribeToOnPlayerDamaged();
    //     OnPlayerDamaged();
    // }

    public void OnPlayerDamaged(ref int currentHealth)
    {
        // if (_playerHealthManager == null) return;
        //
        // _playerCurrentHealth = _playerHealthManager.GetCurrentHealth();

        _playerCurrentHealth = currentHealth;
    }

    // private void SubscribeToOnPlayerDamaged()
    // {
    //     if (_playerHealthManager == null) return;
    //     
    //     _playerHealthManager.OnPlayerDamaged += OnPlayerDamaged;
    // }
}
