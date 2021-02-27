using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Obstacle : MonoBehaviour
{
    public enum obstacles { Horizontal,RotatingPlatform,Rotator,RotatingStick};
    public obstacles obstaclesType;
    public float leftLimit;
    public float rightLimit;
    public float speed;
    public float force;
    public float forceYAxis;
    public float rotateSpeed;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnTriggerEntered(Collider trigger, Collider collider)
    {
        Debug.Log(collider.name + " entered trigger " + trigger.name);
    }
    public void moveObstacle()
    {
        if (obstaclesType == obstacles.Horizontal)
        {
            if (gameObject.transform.position.x < leftLimit)
            {
                speed = speed * -1;
            }
            else if (gameObject.transform.position.x > rightLimit)
            {
                speed = speed * -1;
            }
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (obstaclesType == obstacles.RotatingPlatform)
        {
            transform.Rotate(new Vector3(0, 0, 1)*rotateSpeed);
        }
        else if (obstaclesType == obstacles.Rotator)
        {
            transform.Rotate(new Vector3(0,1,0),5*rotateSpeed);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (obstaclesType == obstacles.Horizontal)
        {
            if (collision.transform.tag=="Player")
            {
                GameManager.Instance.playerState = GameManager.playerStates.onAir;
                ForceObject(collision);
            }
            if (collision.transform.tag=="opponent")
            {
                ForceObject(collision);
            }
        }
        if (obstaclesType == obstacles.RotatingStick)
        {
            if (collision.transform.tag == "Player")
            {
                GameManager.Instance.playerState = GameManager.playerStates.onAir;
                ForceObject(collision);
            }
            if (collision.transform.tag == "opponent")
            {
                ForceObject(collision);
            }
        }
    }


    void ForceObject(Collision collision)
    {
        var direction = (collision.contacts[0].point - collision.gameObject.transform.position).normalized;
        direction = direction * -force;
        direction = new Vector3(direction.x * 2, forceYAxis, direction.z * 2);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(direction);
    }
}
