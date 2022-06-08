using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEnable : MonoBehaviour
{
    [SerializeField]
    private GameObject jump;
    [SerializeField]
    private GameObject speed;

    public bool jumpDestroyed = false;
    public bool speedDestroyed = false;

    // Update is called once per frame
    public void Update()
    {
        if (jumpDestroyed)
        {
            jump.SetActive(true);
        }
        else
        {
            jump.SetActive(false);
        }

        if (speedDestroyed)
        {
            speed.SetActive(true);
        }
        else
        {
            speed.SetActive(false);
        }
    }
}
