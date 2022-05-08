using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //reference variable for alarm clock prefab
    public GameObject alarmClockPrefab;
    public GameObject jumpPrefab;

    //reference variables for player object and clock object
    private GameObject player;
    private GameObject clock;
    private GameObject jump;

    //finds Patroller script on this enemy object
    private Patroller patrolPath;

    //reference variables for starting positions
    private Vector3 alarmClockOriginalPos;
    private Vector3 playerOriginalPos;
    private Vector3 enemyOriginalPos;
    private Vector3 jumpOriginalPos;

    //reference variable for player jump force
    private float playerJumpForce;

    private void Awake()
    {
        //finds player's starting point and jump force in the scene and records it
        player = GameObject.Find("Player");
        playerOriginalPos = player.transform.position;
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerJumpForce = playerScript.jumpForce;

        //finds clock's starting point in the scene and records it
        clock = GameObject.Find("alarmClock");
        alarmClockOriginalPos = clock.transform.position;

        //finds jump boost's starting point in the scene and records it
        jump = GameObject.Find("powerup_jump");
        jumpOriginalPos = jump.transform.position;

        //finds enemy's starting point in the scene and records it
        //patrolPath = GetComponent<Patroller>();
        //enemyOriginalPos = transform.position;
    }

    void RestartObjects()
    {
        //resets the player's position
        player.transform.position = playerOriginalPos;
        //resets player jump force
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerScript.SetJumpForce(playerJumpForce);

        //resets this enemy object's position and their patrol path
        //transform.position = enemyOriginalPos;
        //patrolPath.nextPoint = 0;

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

        try //Try to find jump and reset its position
        {
            jump = GameObject.Find("powerup_jump");
            jump.transform.position = jumpOriginalPos;
        }
        catch //Makes new jump if none is found, then places it at starting point
        {
            GameObject powerup_jump = Instantiate(jumpPrefab);

            powerup_jump.transform.position = jumpOriginalPos;
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
