using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JumpPW : MonoBehaviour {
    public float increase = 5f;

    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player" && isUsed == false) {
            GameObject player = collision.gameObject;
            PlayerController playerScript = player.GetComponent<PlayerController>();
            GameObject boostEnable = GameObject.Find("UIEnable");
            UIEnable boostUI = boostEnable.GetComponent<UIEnable>();

            if (playerScript) {
                playerScript.ChangeJumpForce(increase);
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("Boost");

                boostUI.jumpDestroyed = true;
            }

            isUsed = true;

        }
    }
}
