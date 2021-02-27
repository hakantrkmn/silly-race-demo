using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public static Action<GameObject, Collision> onCollisionEntered;

    public Transform destination;
    NavMeshAgent agent;
    Vector3 startPos;
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.transform.position ;
    }

    void Update()
    {
        agent.updateRotation = false;
        GetComponent<Animator>().SetFloat("speed", agent.desiredVelocity.magnitude);
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollisionEntered != null)
        {
            onCollisionEntered(gameObject, collision);
        }
    }

    public void DestroyOpponent()
    {
        transform.position = startPos;
        gameObject.GetComponent<Animator>().SetBool("fall", false);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;

    }
}
