using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void ScoreUpdated();
    public event ScoreUpdated OnUpdateScore;
    
    private HealthManager _playerHealthManager;

    public int CurrentScore { get; private set; }

    private void Awake()
    {
        if (FindObjectsOfType<ScoreKeeper>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += HandleScoreReset;
        }
    }

    /* SCORE KEEPING METHODS */
    public void ModifyScore(ref int amount)
    {
        CurrentScore += amount;
        Math.Clamp(CurrentScore, 0, int.MaxValue);
        OnUpdateScore?.Invoke();
    }

    private void ResetScore()
    {
        CurrentScore = 0;
    }

    private void HandleScoreReset(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                Destroy(gameObject);
                break;
            case 1:
                ResetScore();
                break;
        }
    }
}
