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
            if (transform.position != target.position + offset)
            {
                transform.position = target.position + offset;
                transform.rotation = Quaternion.Euler(16, 0, 0);
            }
            FollowPlayer();
        }
        else if (GameManager.Instance.gameState==GameManager.gameStates.paint)
        {
            FollowWall();
        }
        else if (GameManager.Instance.gameState == GameManager.gameStates.start)
        {
            StartMove();
        }
    }

    //kamerayı karaktere doğru hareketini sağlıyoruz
    private void StartMove()
    {
        transform.position = Vector3.Lerp(transform.position, target.position+offset, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(16, 0, 0), Time.deltaTime * lerpSpeed);
    }

    //paint durumuna geçildiğinde kamera hareketi
    private void FollowWall()
    {
        transform.position = Vector3.Lerp(transform.position, finishPosition.position, Time.deltaTime * lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(2, -0.8f, 0), Time.deltaTime * lerpSpeed);
    }

    //oyunucuyu takip etme fonksiyonu
    void FollowPlayer()
    {
        transform.position = target.position + offset;
    }



}
