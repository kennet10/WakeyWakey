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

    //finds Patroller script on this enemy object
    private Patroller patrolPath;

    //reference variables for starting positions
    private Vector3 alarmClockOriginalPos;
    private Vector3 playerOriginalPos;
    private Vector3 enemyOriginalPos;

    private void Awake()
    {
        //finds player's starting point in the scene and records it
        player = GameObject.Find("Player");
        playerOriginalPos = player.transform.position;

        //finds clock's starting point in the scene and records it
        clock = GameObject.Find("alarmClock");
        alarmClockOriginalPos = clock.transform.position;

        //finds enemy's starting point in the scene and records it
        patrolPath = GetComponent<Patroller>();
        enemyOriginalPos = transform.position;
    }

    void RestartObjects()
    {
        //resets the player's position
        player.transform.position = playerOriginalPos;

        //resets this enemy object's position and their patrol path
        transform.position = enemyOriginalPos;
        patrolPath.nextPoint = 0;

        try //Try to find alarm clock and reset its position
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RestartObjects();
        }
    }
}
