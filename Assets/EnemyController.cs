using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    void RestartLevel() //Restarts the level
    {
        GameObject player = GameObject.Find("Player");
        GameObject clock = GameObject.Find("alarmClock_static");
        player.transform.position = new Vector3(-8, -2, 0);
        clock.transform.position = new Vector3(0, 3, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RestartLevel();
        }
    }
}
