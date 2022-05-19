using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //reference variable for desired game object
    private SpriteRenderer spriteRenderer;

    //reference variable for the new sprite
    [SerializeField]
    private Sprite newSprite;

    //reference variable for the original sprite
    [SerializeField]
    private Sprite originalSprite;

    [SerializeField]
    bool triggered;
    [SerializeField]
    public bool lever_trigger;

    // Start is called before the first frame update
    void Start()
    {
        //sets object reference to current game object this script is on
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        //sets bool variables to false
        triggered = false;
        lever_trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered & Input.GetKeyDown(KeyCode.R))
        {
            if (lever_trigger)
            {
                spriteRenderer.sprite = originalSprite;
                lever_trigger = false;
            }
            else
            {
                Debug.Log("Button 'R' has been pressed");
                lever_trigger = true;
            }

            FindObjectOfType<AudioManager>().Play("Lever");

        }

        if (!lever_trigger)
        {
            spriteRenderer.sprite = originalSprite;
        }
        else
        {
            spriteRenderer.sprite = newSprite;
        }

    }

    public void ResetLeverTrigger()
    {
        lever_trigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = true;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            triggered = false;
        }


    }

}
