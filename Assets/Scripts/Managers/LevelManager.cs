using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool paused;
    [SerializeField]
    private GameObject pauseMenu;

    public void Start()
    {
        paused = false;
        Time.timeScale = 1;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                GameStateManager.UnpauseGame();
                pauseMenu.SetActive(false);
                paused = false;
            }
            else
            {
                GameStateManager.PauseGame();
                pauseMenu.SetActive(true);
                paused = true;
            }
        }
    }

    public void RestartLevel()
    {
        GameStateManager.RestartLevel();
        Debug.Log("Click");
    }

    public void QuitToTitle()
    {
        GameStateManager.QuitToTitle();

    }
}
