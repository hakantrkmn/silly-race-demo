using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;
    public Transform target;
    public Transform finishPosition;
    public float lerpSpeed;


    void Update()
    {
        if (GameManager.Instance.gameState==GameManager.gameStates.run)
        {
            FollowPlayer();
        }
        else if (GameManager.Instance.gameState==GameManager.gameStates.paint)
        {
            FollowWall();

        }
    }

    private void FollowWall()
    {
        transform.position = Vector3.Lerp(transform.position, finishPosition.position, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(2, -0.8f, 0), Time.deltaTime * lerpSpeed);
    }

    void FollowPlayer()
    {
        transform.position = target.position + offset;
    }



}
