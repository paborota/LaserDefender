using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundHandler : MonoBehaviour
{
    [SerializeField] private BackgroundScroller bs1;
    [SerializeField] private BackgroundScroller bs2;

    [SerializeField] private float scrollSpeedMultiplierInMenu = 0.5f;
    [SerializeField] [Range(0.0f, 1.0f)] private float lerpOfSpeedChange = 0.5f;
    private void Awake()
    {
        if (FindObjectsOfType<BackgroundHandler>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            bs1.LerpSpeed = lerpOfSpeedChange;
            bs2.LerpSpeed = lerpOfSpeedChange;
            AlterScrollSpeed(scrollSpeedMultiplierInMenu);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 0:
                LerpScrollSpeed(scrollSpeedMultiplierInMenu);
                break;
            case 1:
                ResetScrollSpeed();
                break;
            case 2:
                LerpScrollSpeed(scrollSpeedMultiplierInMenu);
                break;
        }
    }

    private void AlterScrollSpeed(float multiplier)
    {
        if (bs1 == null && bs2 == null) return;
        
        bs1.AlterScrollSpeed(ref multiplier);
        bs2.AlterScrollSpeed(ref multiplier);
    }

    private void LerpScrollSpeed(float multiplier)
    {
        if (bs1 == null && bs2 == null) return;
        
        bs1.LerpScrollSpeed(ref multiplier);
        bs2.LerpScrollSpeed(ref multiplier);
    }

    private void ResetScrollSpeed()
    {
        if (bs1 == null && bs2 == null) return;
        
        bs1.ResetScrollSpeed();
        bs2.ResetScrollSpeed();
    }
}
