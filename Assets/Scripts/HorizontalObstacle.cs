using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HorizontalObstacle : Obstacle
{

    void Start()
    {
        obstaclesType = obstacles.Horizontal;
        Opponent.onCollisionEntered+= OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.GetComponent<Animator>().SetBool("fall", true);
            gameObj.GetComponent<Animator>().SetBool("run", false);
            GameManager.Instance.playerState = GameManager.playerStates.onAir;
            ForceObject(collision,gameObj);
        }
    }

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
