using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float lerpSpeed;
    void Start()
    {
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    //playerın harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.transform.parent.parent = null;
            GameManager.Instance.playerState = GameManager.playerStates.onGround;
            gameObj.GetComponent<Animator>().SetBool("fall", false);
        }
    }
    //opponentin harekete geçirdiği actiona göre kontrol yapıyoruz. eğer çarptığı obstacle bu nesne ise gerekli işlemleri yapıyoruz
    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.transform.parent = null;
            GameManager.Instance.playerState = GameManager.playerStates.onGround;
        }
    }

}
