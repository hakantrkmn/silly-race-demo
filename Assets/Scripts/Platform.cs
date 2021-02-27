using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Opponent.onCollisionEntered += OnCollisionEntered;
        PlayerController.onPlayerCollisionEntered += OnPlayerCollisionEntered;
    }

    private void OnPlayerCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.transform.parent.parent = null;
            GameManager.Instance.playerState = GameManager.playerStates.onGround;
            gameObj.GetComponent<Animator>().SetBool("fall", false);
        }
    }

    private void OnCollisionEntered(GameObject gameObj, Collision collision)
    {
        if (collision.gameObject==gameObject)
        {
            gameObj.transform.parent = null;
        }
    }

}
