using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroy : MonoBehaviour
{
    public GameObject Door;
    private Interaction lever_interact;

    // Start is called before the first frame update
    void Start()
    {
        lever_interact = Door.GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever_interact.lever_trigger == true)
        {
            Debug.Log("Door has been destroyed");
            Destroy(gameObject);
        }
    }
}