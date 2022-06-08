using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    //reference variable for alarm clock prefab and powerup prefabs
    public GameObject alarmClockPrefab, jumpPrefab, speedPrefab, doorPrefab;

    //reference variables for player object clock object, powerup objects, door, and lever
    private GameObject player, clock, jump, speed, door, lever, boostEnable;

    //finds Patroller script on this enemy object
    private Patroller patrolPath;

    //reference variables for starting positions of all objects
    private Vector3 alarmClockOriginalPos;
    private Vector3 playerOriginalPos;
    private Vector3 enemyOriginalPos;
    private Vector3 jumpOriginalPos;
    private Vector3 speedOriginalPos;
    private Vector3 doorOriginalPos;

    //reference variable for player jump force
    private float playerJumpForce, playerMoveSpeed;

    //is there a power up in the level?
    private bool isJump;
    private bool isSpeed;
    private bool isDoor;
    
    //sets up variable to store Interaction script
    private Interaction lever_interact;

    //sets up variable to store door lists
    private List<string> doorLeversTrue = new List<string>();
    private List<string> doorLeversFalse = new List<string>();

    //sets up AudioManager
    private AudioManager aManEnemy;

    private void Awake()
    {
        //Gets jumpDestroyed and speedDestroyed to in UIEnable
        boostEnable = GameObject.Find("UIEnable");

        //finds player's starting point in the scene and records it
        player = GameObject.Find("Player");
        playerOriginalPos = player.transform.position;
        PlayerController playerScript = player.GetComponent<PlayerController>();
        //finds player's startin jump force in the scene and records it
        playerJumpForce = playerScript.jumpForce;
        //finds player's starting move speed in the scene and records it
        playerMoveSpeed = playerScript.moveSpeed;

        //finds clock's starting point in the scene and records it
        clock = GameObject.Find("alarmClock");
        alarmClockOriginalPos = clock.transform.position;

        if (GameObject.Find("powerup_jump") == true)
        {
            isJump = true;

            //finds jump boost's starting point in the scene and records it
            jump = GameObject.Find("powerup_jump");
            jumpOriginalPos = jump.transform.position;

        }

        if (GameObject.Find("powerup_speed") == true)
        {
            isSpeed = true;

            //finds jump boost's starting point in the scene and records it
            speed = GameObject.Find("powerup_speed");
            speedOriginalPos = speed.transform.position;

        }

        if (GameObject.Find("Door") == true)
        {
            isDoor = true;

            //finds door's starting point in the scene and records it
            door = GameObject.Find("Door");
            doorOriginalPos = door.transform.position;
            //gets list for LeversTrue and LeversFalse from door
            DoorDestroy doorScript = door.GetComponent<DoorDestroy>();
            doorLeversTrue = doorScript.m_LeversTrue;
            doorLeversFalse = doorScript.m_LeversFalse;

        }

        if (GameObject.Find("Interaction") == true)
        {
            //Getting lever interactin script
            lever = GameObject.Find("Interaction");
            lever_interact = lever.GetComponent<Interaction>();

        }

        //finds enemy's starting point in the scene and records it
        patrolPath = GetComponent<Patroller>();
        enemyOriginalPos = transform.position;

    }

    private void Start()
    {
        aManEnemy = FindObjectOfType<AudioManager>();
    }

    void RestartObjects()
    {
        UIEnable boostUI = boostEnable.GetComponent<UIEnable>();

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

            boostUI.jumpDestroyed = false;
        }

        //for the speed powerup prefab
        if (isSpeed == true)
        {
            try //Try to find speed and reset its position
            {
                speed = GameObject.Find("powerup_speed");
                speed.transform.position = speedOriginalPos;
            }
            catch //Makes new speed if none is found, then places it at starting point
            {

                GameObject powerup_speed = Instantiate(speedPrefab);

                //necessary to prevent multiple prefabs of powerup to spawn
                powerup_speed.name = "powerup_speed";

                powerup_speed.transform.position = speedOriginalPos;
            }

            boostUI.speedDestroyed = false;
        }

        //for the Door prefab
        if (isDoor == true)
        {
            try //Try to find Door and reset its position
            {
                door = GameObject.Find("Door");
                door.transform.position = doorOriginalPos;
            }
            catch //Makes new Door if none is found, then places it at starting point
            {

                GameObject Door = Instantiate(doorPrefab);

                //necessary to prevent multiple prefabs of powerup to spawn
                Door.name = "Door";

                Door.transform.position = doorOriginalPos;

                //giving the new door the lever lists
                DoorDestroy DoorScript = Door.GetComponent<DoorDestroy>();
                DoorScript.m_LeversTrue = doorLeversTrue;
                DoorScript.m_LeversFalse = doorLeversFalse;
            }
        }

        //resets lever_trigger of Interaction to false
        foreach (string lvr in doorLeversTrue)
        {
            lever = GameObject.Find(lvr);
            lever_interact = lever.GetComponent<Interaction>();
            lever_interact.ResetLeverTrigger();
        }

        if (doorLeversFalse != null)
        {
            foreach (string lvr in doorLeversFalse)
            {
                lever = GameObject.Find(lvr);
                lever_interact = lever.GetComponent<Interaction>();
                lever_interact.ResetLeverTrigger();
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aManEnemy.Play("Reset");
            RestartObjects();
        }
    }
}
