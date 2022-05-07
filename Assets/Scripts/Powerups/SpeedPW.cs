using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpeedPW : MonoBehaviour {
    public float increase = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameObject player = collision.gameObject;
            PlayerController playerScript = player.GetComponent<PlayerController>();

            if (playerScript) {
                playerScript.ChangeMoveSpeed(increase);
                Destroy(gameObject);
            }
        }
    }
}
