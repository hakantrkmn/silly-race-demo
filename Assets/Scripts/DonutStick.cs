using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DonutStick : Obstacle
{

    void Start()
    {
        timer = 0;
        wait = false;
        PointTwo = transform.localPosition;
        pointThree = PointTwo;
        obstaclesType = obstacles.DonutStick;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    //player collidera girerse yapılacaklar
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject == gameObject)
        {
            GameManager.Instance.opponentState = GameManager.opponentStates.onAir;
            gameObj.GetComponent<NavMeshAgent>().isStopped = true;
            gameObj.GetComponent<Animator>().SetBool("fall", true);
            ForceObject(collision, gameObj);
        }
    }



    void Update()
    {
        //timer tutuyoruz. timer her belirlediğimiz değere geldiğinde timer duruyor ve obstacle hareket ediyor
        timer += Time.deltaTime;
        if (timer>=timeInterval && wait==false)
        {
            moveObstacle();
        }
        else if (wait)
        {
            timer = 0;
            wait = false;
        }

    }
}
