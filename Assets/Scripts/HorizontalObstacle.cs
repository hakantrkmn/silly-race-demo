using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorizontalObstacle : Obstacle
{

    void Start()
    {
        PointTwo = transform.position;
        obstaclesType = obstacles.Horizontal;
        Opponent.onCollisionEntered+= OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }
    //playerın harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.GetComponent<Animator>().SetBool("run", false);
            gameObj.GetComponent<Animator>().SetBool("fallEnd", false);
            gameObj.GetComponent<Animator>().SetBool("fall", true);
            gameObj.GetComponent<Animator>().SetTrigger("fallTrigger");
            GameManager.Instance.playerState = GameManager.playerStates.onAir;
            ForceObject(collision,gameObj);
        }
    }
    //opponentin harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            GameManager.Instance.opponentState = GameManager.opponentStates.onAir;
            gameObj.GetComponent<NavMeshAgent>().isStopped = true;
            gameObj.GetComponent<Animator>().SetBool("fall", true);
            ForceObject(collision,gameObj);
        }
    }

    void Update()
    {
        moveObstacle();
    }

}
