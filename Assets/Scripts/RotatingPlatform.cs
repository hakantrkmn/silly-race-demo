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

    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {

            gameObj.transform.parent.parent = collision.gameObject.transform;
        }
    }

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
    //private void OnCollisionStay(Collision collision)
    //{
    //    var direction = new Vector3(collision.contacts[0].normal.y, 1.5f, 0);
    //    collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * force, ForceMode.Impulse);
    //}
}
