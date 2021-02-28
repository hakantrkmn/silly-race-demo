using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Obstacle : MonoBehaviour
{
    public enum obstacles { Horizontal, RotatingPlatform, Rotator, RotatingStick };
    public obstacles obstaclesType;
    [Header("HorizontalObstacle Settings")]
    public float leftLimit;
    public float rightLimit;
    public float speed;
    [Header("Force Object Settings")]
    public float force;
    public float forceYAxis;
    [Header("Rotation Settings")]
    public float rotateSpeed;


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
            transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed);
        }
        else if (obstaclesType == obstacles.Rotator)
        {
            transform.Rotate(new Vector3(0, 1, 0), 5 * rotateSpeed);
        }
    }


    public void ForceObject(Collision collision, GameObject gameObj)
    {
        var direction = (collision.contacts[0].point - collision.gameObject.transform.position).normalized;
        direction = (direction * force).normalized;
        direction = new Vector3(direction.x * 2, forceYAxis, direction.z * 2);
        gameObj.GetComponent<Rigidbody>().AddForce(direction * force,ForceMode.Impulse);
    }

}
