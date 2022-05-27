using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlpha : MonoBehaviour
{
    //alpha of object when player is within range
    [SerializeField] private float alphaNum;

    //reference variable to hold spriteRenderer to change
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //sets object reference to current game object this script is on
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    //when player is within range object becomes transparent
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = spriteRenderer.color;
            color.a = alphaNum;
            spriteRenderer.color = color;
        }


    }

    //when player is out of range object becomes opaque
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Color color = spriteRenderer.color;
            //sets color back to opaque
            color.a = 1;
            spriteRenderer.color = color;
        }


    }
}
