using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRecorder : MonoBehaviour
{
    public static int previousLevel = 0;
    //records level before game over
    public static void SetPreviousLevel()
    {
        previousLevel = GameStateManager.GetCurrentLevel();
        Debug.Log("Previous Level set:" + previousLevel);
    }

    //goes back to previous level
    public static void PreviousLevel()
    {
        GameStateManager.RestartPreviousLevel(previousLevel);
        Debug.Log("Previous Level: " + previousLevel);
    }
}
