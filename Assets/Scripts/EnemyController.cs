using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //reference variable for alarm clock prefab
    public GameObject alarmClockPrefab;

    //reference variables for player object and clock object
    private GameObject player;
    private GameObject clock;

    //reference variables for starting positions
    private Vector3 alarmClockOriginalPos;
    private Vector3 playerOriginalPos;

    private void Awake()
    {
        //finds player's starting point in the scene and records it
        player = GameObject.Find("Player");
        playerOriginalPos = player.transform.position;

        //finds clock's starting point in the scene and records it
        clock = GameObject.Find("alarmClock");
        alarmClockOriginalPos = clock.transform.position;
    }

    void RestartObjects() //Puts Objects and Players Back to Starting Positions
    {
        player.transform.position = playerOriginalPos;

        try //Try to find alarm clock and reset its location
        {
            clock = GameObject.Find("alarmClock");
            clock.transform.position = alarmClockOriginalPos;
        }
        catch //Makes new alarm clock if none is found, then places it at starting point
        {
            GameObject alarmClock = Instantiate(alarmClockPrefab);

            //This below is necessary for door open/close to work properly
            alarmClock.name = "alarmClock";

            alarmClock.transform.position = alarmClockOriginalPos;
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
