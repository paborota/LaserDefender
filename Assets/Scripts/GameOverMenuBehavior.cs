using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenuBehavior : MonoBehaviour
{
    [Tooltip("Text Mesh Pro object that displays the ending score.")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        var scoreKeeper = FindObjectOfType<ScoreKeeper>();
        if (scoreKeeper == null) return;

        scoreText.text = scoreKeeper.CurrentScore.ToString();
    }
}
