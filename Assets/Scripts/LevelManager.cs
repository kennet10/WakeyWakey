using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private bool paused = false;
    [SerializeField]
    private GameObject pauseMenu;

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
    }

    public void QuitToTitle()
    {
        GameStateManager.QuitToTitle();
    }
}
