using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPlayer : MonoBehaviour
{
    public float slowedSpeed;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();

            playerControl.moveSpeed = slowedSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControl playerControl = other.GetComponent<PlayerControl>();
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();
            if (!playerAttack.spedUp)
                playerControl.moveSpeed = playerControl.runSpeed;
            else
            {
                playerControl.moveSpeed = playerAttack.speedupSpeed;
            }
        }
    }
}