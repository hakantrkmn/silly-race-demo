using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    public Transform destination;
    NavMeshAgent agent;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.transform.position ;
    }

    // Update is called once per frame
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
        if (collision.transform.tag == "HorizontalObstacle")
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<Animator>().SetBool("fall", true);  
            Instantiate(gameObject, startPos, Quaternion.identity,transform.parent);
        }
        else if (collision.transform.tag == "rotatingPlatform")
        {
            transform.parent = collision.gameObject.transform;

        }
        else if (collision.transform.tag == "platform")
        {
            transform.parent = null;
        }
    }

    public void DestroyOpponent()
    {
            Destroy(gameObject);
    }
}
