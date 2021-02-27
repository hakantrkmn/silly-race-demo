using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : Obstacle
{

    // Start is called before the first frame update
    void Start()
    {
        obstaclesType = obstacles.RotatingStick;
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            ForceObject(collision,gameObj);
        }
    }

    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject == gameObject)
        {
            ForceObject(collision, gameObj);
        }
    }


}
