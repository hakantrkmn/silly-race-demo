using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : Obstacle
{
    void Start()
    {
        obstaclesType = obstacles.RotatingPlatform;
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }
    //player collidera girerse yapılacaklar
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.transform.parent.parent = collision.gameObject.transform;
        }
    }

    //opponent collidera girerse yapılacaklar
    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {

            gameObj.transform.parent = collision.gameObject.transform;
        }
    }

    void Update()
    {
        moveObstacle();
    }
}
