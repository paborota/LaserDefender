using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Time after GameOver is called before loading Game Over scene.")]
    [SerializeField] private float timeBeforeGameOver = 1.0f;
    
    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadGameOverMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }
    
    public void GameOver()
    {
        StartCoroutine(StartGameOver());
    }

    private IEnumerator StartGameOver()
    {
        yield return new WaitForSecondsRealtime(timeBeforeGameOver);
        LoadGameOverMenu();
    }
}
