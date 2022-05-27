using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    //reference variable for desired game object
    private SpriteRenderer spriteRenderer;

    //reference variable for the new sprite
    [SerializeField]
    private Sprite newSprite;

    //reference variable for the original sprite
    [SerializeField]
    private Sprite originalSprite;

    //boolean variable to check if door is open
    private bool isOpen;

    //boolean variabel to check if player has pressed the key already
    private bool isPressed;

    private GameObject portalHint;

    private void Start()
    {
        //sets object reference to current game object this script is on
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //sets boolean to false from the start
        isPressed = false;

    }

    //opens the portal: changes sprite and sets bool to true
    void OpenDoor()
    {
        spriteRenderer.sprite = newSprite;
        isOpen = true;
    }

    //closes the portal: changes sprite and sets bool to false
    void CloseDoor()
    {
        spriteRenderer.sprite = originalSprite;
        isOpen = false;
    }

    private void Update()
    {
        //if the alarm clock is present, it will close the portal. if it
        //is not present, the portal will open.
        if (GameObject.Find("alarmClock") == false)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    //if player stays within the portal's collider, they can press "R" to enter
    //the next level. this only works when the portal is open, and only once
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<AudioManager>().Play("Portal");

            if (Input.GetKey(KeyCode.R) == true && isOpen == true && isPressed == false)
            {

                GameStateManager.NextLevel();

                Debug.Log("You have been sent to the next level.");
                isPressed = true;
            }

            //supposed to show hint when player gets close to closed portal
            //if (!isOpen)
            //{
            //    portalHint = GameObject.Find("PortalHint");
            //    portalHint.SetActive(true);
            //}
        }
    }
}
    
