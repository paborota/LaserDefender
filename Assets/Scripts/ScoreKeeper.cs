using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public delegate void ScoreUpdated();
    public event ScoreUpdated OnUpdateScore;
    
    
    private HealthManager _playerHealthManager;

    public int CurrentScore { get; private set; }

    
    /* SCORE KEEPING METHODS */
    public void ModifyScore(ref int amount)
    {
        CurrentScore += amount;
        Math.Clamp(CurrentScore, 0, int.MaxValue);
        OnUpdateScore?.Invoke();
    }
}
