using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HalfDonut : Obstacle
{
    void Start()
    {
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    //opponent collidera girerse yapılacaklar
    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            GameManager.Instance.opponentState = GameManager.opponentStates.onAir;
            gameObj.GetComponent<NavMeshAgent>().isStopped = true;
            gameObj.GetComponent<Animator>().SetBool("fall", true);
        }

    }

    //player collidera girerse yapılacaklar
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.GetComponent<Animator>().SetBool("fallEnd", false);
            gameObj.GetComponent<Animator>().SetBool("run", false);
            gameObj.GetComponent<Animator>().SetBool("fall", true);
            gameObj.GetComponent<Animator>().SetTrigger("fallTrigger");
            GameManager.Instance.playerState = GameManager.playerStates.onAir;
            ForceObject(collision, gameObj);
        }
    }

}
