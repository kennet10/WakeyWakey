using UnityEngine;

public class SceneChange : MonoBehaviour
{

    //Start the next level
    public static void NextLevel()
    {
        GameStateManager.NextLevel();
    }
}
