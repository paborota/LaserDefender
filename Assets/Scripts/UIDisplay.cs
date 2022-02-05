using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private HealthManager playerHealthManager;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private ScoreKeeper _scoreKeeper;
    
    

    private void Awake()
    {
        if (playerHealthManager == null) return;
        
        playerHealthManager.OnPlayerHealthUpdated += UpdateHealth;
        healthSlider.maxValue = playerHealthManager.StartingHealth;
    }

    private void Start()
    {
        SetupScoreKeeperLink();
        UpdateScore();
        UpdateHealth();
    }

    private void SetupScoreKeeperLink()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (_scoreKeeper == null) return;
        
        _scoreKeeper.OnUpdateScore += UpdateScore;
    }

    private void UpdateScore()
    {
        if (_scoreKeeper == null) return;
        
        scoreText.text = _scoreKeeper.CurrentScore.ToString();
    }

    private void UpdateHealth()
    {
        if (playerHealthManager == null) return;
        
        healthSlider.value = playerHealthManager.CurrentHealth;
    }
}
