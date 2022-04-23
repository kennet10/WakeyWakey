using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public GameObject alarmClock;

    void RestartObjects() //Puts Objects and Players Back to Starting Positions
    {
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(-8, -2, 0);

        try
        {
            GameObject clock = GameObject.Find("alarmClock");
            clock.transform.position = new Vector3(0, 3, 0);
        }
        catch
        {
            GameObject alarmClock_static = Instantiate(alarmClock);
            alarmClock_static.transform.position = new Vector3(0, 3, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RestartObjects();
        }
    }
}
