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
    public GameObject parent;

    public float EndDistance;

    void Start()
    {
        //ai'ın hedefini belirliyoruz. parent objesini rotating stickte değiştimiz için eski haline getirebilmek için kaydediyoruz.
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
        transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
    }

    //opponent herhangi bir collisiona girdiğinde actionu harekete geçiriyoruz

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollisionEntered != null)
        {
            onCollisionEntered(gameObject, collision);
        }
    }
    void CalculateDistance()
    {
        EndDistance = (destination.transform.position - transform.position).magnitude;
    }

    //opponnent eğer obstacleye çarparsa animasyonda sağladığımız event sayesinde animasyonun sonunda bu fonksiyonu çağırıyor.
    public void DestroyOpponent()
    {
        transform.position = startPos;
        gameObject.GetComponent<Animator>().SetBool("fall", false);
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;

    }
}
