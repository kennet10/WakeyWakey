using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] public List<string> m_Levels = new List<string>();
    [SerializeField] public string m_TitleScreenName;
    [SerializeField] public string m_GameOverScreenName, m_VictoryScreenName;

    public int currentLevel;

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

    public static int GetCurrentLevel()
    {
        return _instance.currentLevel;
    }

    //Start a new game
    public static void NewGame()
    {
        m_State = GAMESTATE.PLAYING;

        _instance.currentLevel = 0;

        if (_instance.m_Levels.Count > 0)
        {
            Debug.Log("Level Index:" + _instance.currentLevel);
            SceneManager.LoadScene(_instance.m_Levels[_instance.currentLevel]);
        }
    }

    //Start the next level
    public static void NextLevel()
    {
        m_State = GAMESTATE.PLAYING;
        _instance.currentLevel++;

        if (_instance.m_Levels.Count > _instance.currentLevel)
        {
            Debug.Log("Level Index:" + _instance.currentLevel);
            SceneManager.LoadScene(_instance.m_Levels[_instance.currentLevel]);
        }
        else
        {
            SceneManager.LoadScene(_instance.m_VictoryScreenName);
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

    //Restarts previous level
    public static void RestartPreviousLevel(int previousLevel)
    {
        Debug.Log("Previous Level Index: " + previousLevel);
        SceneManager.LoadScene(_instance.m_Levels[previousLevel]);

        if (_instance.m_Levels.Count <= _instance.currentLevel)
        {
            Debug.Log("Level Index:" + _instance.currentLevel);
            SceneManager.LoadScene(_instance.m_TitleScreenName);
        }
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

    //QuitGame
    public static void QuitGame()
    {
        Application.Quit();
    }

}
