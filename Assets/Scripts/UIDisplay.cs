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
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (_scoreKeeper != null)
        {
            _scoreKeeper.OnUpdateScore += UpdateScore;
        }

        if (playerHealthManager != null)
        {
            playerHealthManager.OnPlayerHealthUpdated += UpdateHealth;
        }
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealthManager.StartingHealth;

        UpdateScore();
        UpdateHealth();
    }

    private void UpdateScore()
    {
        scoreText.text = _scoreKeeper.CurrentScore.ToString();
    }

    private void UpdateHealth()
    {
        healthSlider.value = playerHealthManager.CurrentHealth;
    }
}
