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
    GameObject parent;
    Rigidbody rb;


    bool death;
    public float EndDistance;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //ai'ın hedefini belirliyoruz. parent objesini rotating platformda değiştimiz için eski haline getirebilmek için kaydediyoruz.
        parent = transform.parent.gameObject;
        agent = GetComponent<NavMeshAgent>();
        if (GameManager.Instance.gameState==GameManager.gameStates.start)
        {
            agent.isStopped = true;
        }
        startPos = transform.position;
        agent.destination = destination.transform.position ;
    }

    void Update()
    {
        if (transform.parent==null)
        {
            transform.parent = parent.transform;
        }

        CalculateDistance();

        if (GameManager.Instance.gameState == GameManager.gameStates.run && GameManager.Instance.opponentState==GameManager.opponentStates.onGround)
        {
            agent.isStopped = false;
        }
        agent.updateRotation = false;
        //opponentin animasyonunu yapmak için sahip olduğu hızı animatore yolluyoruz.
        GetComponent<Animator>().SetFloat("speed", agent.desiredVelocity.magnitude);
    }

    //ai'ın dönme hareketi
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(agent.desiredVelocity.normalized), 30 * Time.deltaTime);
    }

    //opponent herhangi bir collisiona girdiğinde actionu harekete geçiriyoruz

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollisionEntered != null)
        {
            onCollisionEntered(gameObject, collision);
        }
        if (collision.transform.tag=="finish")
        {
            agent.enabled = false;
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
    void CalculateDistance()
    {
        EndDistance = (destination.transform.position - transform.position).magnitude;
    }

    //opponnent eğer obstacleye çarparsa animasyonda sağladığımız event sayesinde animasyonun sonunda bu fonksiyonu çağırıyor.
    public void DestroyOpponent()
    {
        var inGO = Instantiate(gameObject, startPos, Quaternion.identity, transform.parent);
        Ranking.Instance.updateRank(gameObject, inGO);
        Destroy(gameObject);

    }

}
