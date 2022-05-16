using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //reference variable for alarm clock prefab and powerup prefabs
    public GameObject alarmClockPrefab;
    public GameObject jumpPrefab;
    public GameObject speedPrefab;

    //reference variables for player object and clock object, and the powerup objects
    private GameObject player;
    private GameObject clock;
    private GameObject jump;
    private GameObject speed;


    //finds Patroller script on this enemy object
    private Patroller patrolPath;

    //reference variables for starting positions of all objects
    private Vector3 alarmClockOriginalPos;
    private Vector3 playerOriginalPos;
    private Vector3 enemyOriginalPos;
    private Vector3 jumpOriginalPos;
    private Vector3 speedOriginalPos;

    //reference variable for player jump force
    private float playerJumpForce, playerMoveSpeed;

    //is there a power up in the level?
    private bool isJump;
    private bool isSpeed;

    private void Awake()
    {
        //finds player's starting point in the scene and records it
        player = GameObject.Find("Player");
        playerOriginalPos = player.transform.position;
        PlayerController playerScript = player.GetComponent<PlayerController>();
        Debug.Log("Recorded Player pos");
        //finds player's startin jump force in the scene and records it
        playerJumpForce = playerScript.jumpForce;
        Debug.Log("Recorded Player jump");
        //finds player's starting move speed in the scene and records it
        playerMoveSpeed = playerScript.moveSpeed;
        Debug.Log("Recorded Player move");

        //finds clock's starting point in the scene and records it
        clock = GameObject.Find("alarmClock");
        alarmClockOriginalPos = clock.transform.position;
        Debug.Log("Recorded Alarm pos");

        if (GameObject.Find("powerup_jump") == true)
        {
            isJump = true;

            //finds jump boost's starting point in the scene and records it
            jump = GameObject.Find("powerup_jump");
            jumpOriginalPos = jump.transform.position;
            Debug.Log("Recorded Jump pos");

        }

        if (GameObject.Find("powerup_speed") == true)
        {
            isSpeed = true;

            //finds jump boost's starting point in the scene and records it
            speed = GameObject.Find("powerup_speed");
            speedOriginalPos = speed.transform.position;
            Debug.Log("Recorded Speed pos");

        }

        //finds enemy's starting point in the scene and records it
        patrolPath = GetComponent<Patroller>();
        enemyOriginalPos = transform.position;

    }

    void RestartObjects()
    {
        Debug.Log("Restart");
        //resets the player's position
        player.transform.position = playerOriginalPos;
        //resets player jump force
        PlayerController playerScript = player.GetComponent<PlayerController>();
        playerScript.SetJumpForce(playerJumpForce);
        //resets player move speed
        playerScript.SetMoveSpeed(playerMoveSpeed);

        try //resets this enemy object's position and their patrol path if it is recorded
        {
            transform.position = enemyOriginalPos;
            patrolPath.nextPoint = 0;
        }
        catch
        {
            Debug.Log("no enemy reset");
        }

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

        //for the jump powerup prefab
        if (isJump == true)
        {
            try //Try to find jump and reset its position
            {
                jump = GameObject.Find("powerup_jump");
                jump.transform.position = jumpOriginalPos;
            }
            catch //Makes new jump if none is found, then places it at starting point
            {
                
                GameObject powerup_jump = Instantiate(jumpPrefab);

                //necessary to prevent multiple prefabs of powerup to spawn
                powerup_jump.name = "powerup_jump";

                powerup_jump.transform.position = jumpOriginalPos;
            }
        }

        //for the speed powerup prefab
        if (isSpeed == true)
        {
            try //Try to find speed and reset its position
            {
                speed = GameObject.Find("powerup_speed");
                speed.transform.position = speedOriginalPos;
            }
            catch //Makes new jump if none is found, then places it at starting point
            {

                GameObject powerup_speed = Instantiate(speedPrefab);

                //necessary to prevent multiple prefabs of powerup to spawn
                powerup_speed.name = "powerup_speed";

                powerup_speed.transform.position = speedOriginalPos;
            }
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
