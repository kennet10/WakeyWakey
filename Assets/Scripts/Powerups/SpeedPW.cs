using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedPW : MonoBehaviour {
    public float increase = 1.5f;

    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.tag == "Player" && isUsed == false) {
            GameObject player = collision.gameObject;
            PlayerController playerScript = player.GetComponent<PlayerController>();

            if (playerScript) {
                playerScript.ChangeMoveSpeed(increase);
                Destroy(gameObject);
                FindObjectOfType<AudioManager>().Play("Boost");
            }

            isUsed = true;

        }

    }
}
