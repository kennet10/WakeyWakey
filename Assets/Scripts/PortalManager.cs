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


    private void Start()
    {
        //sets object reference to current game object this script is on
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

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
    //the next level. this only works when the portal is open
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (Input.GetKey(KeyCode.R) && isOpen == true)
            {
                GameStateManager.NextLevel();
                Debug.Log("You have been sent to the next level.");
            }
        }
    }
}
    
