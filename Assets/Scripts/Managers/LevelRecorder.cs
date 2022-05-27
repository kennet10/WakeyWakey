using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelRecorder : MonoBehaviour
{
    //variable to hold previous level index
    private static int tryAgainLevel;


    //records level before game over
    private void Awake()
    {
    }

    //sets current level
    public static void SetTryAgainLevel()
    {

        Debug.Log("Try Again Level before:" + tryAgainLevel);
        tryAgainLevel = GameStateManager.GetCurrentLevel();
        Debug.Log("Try Again Level set:" + tryAgainLevel);
    }

    //goes back to previous level
    public static void TryAgainLevel()
    {
        Debug.Log("Try Again Level: " + tryAgainLevel);
        GameStateManager.RestartPreviousLevel(tryAgainLevel);
    }
}
