using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private List<string> m_Levels = new List<string>();
    [SerializeField] private string m_TitleScreenName;
    [SerializeField] private string m_GameOverScreenName;

    private static GameStateManager _instance;

    //States of the game
    enum GAMESTATE
    {
        TITLE,
        PLAYING,
        PAUSED,
        GAMEOVER,
    }

    private static GAMESTATE m_State;

    private void Awake()
    {
        //Create the instance
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
        else
        {
            Destroy(this);
        }
    }

    //Start a new game
    public static void NewGame()
    {
        m_State = GAMESTATE.PLAYING;
        if (_instance.m_Levels.Count > 0)
        {
            SceneManager.LoadScene(_instance.m_Levels[0]);
        }
    }

    //Pause the game
    public static void PauseGame()
    {
        m_State = GAMESTATE.PAUSED;
        Time.timeScale = 0;
    }

    //Unpause the game
    public static void UnpauseGame()
    {
        m_State = GAMESTATE.PLAYING;
        Time.timeScale = 1;
    }

    //Restart current level
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //End the game
    public static void EndGame()
    {
        m_State = GAMESTATE.GAMEOVER;
        SceneManager.LoadScene(_instance.m_GameOverScreenName);
    }

    //QuitToTitle
    public static void QuitToTitle()
    {
        m_State = GAMESTATE.TITLE;
        SceneManager.LoadScene(_instance.m_TitleScreenName);
    }
}
