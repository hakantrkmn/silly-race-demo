using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonutStick : Obstacle
{
    void Start()
    {
        obstaclesType = obstacles.DonutStick;
        //Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
        InvokeRepeating("RandomSpeed", 0, 2);
    }

    //donut her 2 saniyede bir random bir dönme hızına sahip olmasını sağlanayan fonksiyon
    void RandomSpeed()
    {
        rotateSpeed = UnityEngine.Random.Range(0.5f, -0.5f);
    }

    private void OnPlayerCollisionEntered(GameObject arg1, Collision arg2)
    {
    }

    private void OnCollisionEntered(GameObject arg1, Collision arg2)
    {
    }

    void Update()
    {
        moveObstacle();
    }
}
