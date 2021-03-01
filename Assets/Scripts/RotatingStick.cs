using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RotatingStick : Obstacle
{

    void Start()
    {
        obstaclesType = obstacles.RotatingStick;
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    //playerın harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            ForceObject(collision,gameObj);
        }
    }
    //opponentin harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject == gameObject)
        {
            gameObj.GetComponent<NavMeshAgent>().isStopped=true;
            ForceObject(collision, gameObj);
        }
    }


}
