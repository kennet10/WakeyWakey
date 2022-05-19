using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRecorder : MonoBehaviour
{
    //variable to hold previous level index
    private int tryAgainLevel;

    private static LevelRecorder _instance;

    //records level before game over
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
        }
    }

    //sets current level
    public static void SetTryAgainLevel()
    {

        Debug.Log("Try Again Level before:" + _instance.tryAgainLevel);
        _instance.tryAgainLevel = GameStateManager.GetCurrentLevel();
        Debug.Log("Try Again Level set:" + _instance.tryAgainLevel);
    }

    //goes back to previous level
    public static void TryAgainLevel()
    {
        Debug.Log("Try Again Level: " + _instance.tryAgainLevel);
        GameStateManager.RestartPreviousLevel(_instance.tryAgainLevel);
    }
}
