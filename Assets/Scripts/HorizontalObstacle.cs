using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacle : MonoBehaviour
{
    public float leftLimit;
    public float rightLimit;
    public float speed;
    public float force;
    public float forceYAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveObstacle();
    }

    public void moveObstacle()
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

    private void OnCollisionEnter(Collision collision)
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

    void ForceObject(Collision collision)
    {
        var direction = (collision.contacts[0].point - collision.gameObject.transform.position).normalized;
        direction = direction * -force;
        direction = new Vector3(direction.x * 2, forceYAxis, direction.z * 2);
        collision.gameObject.GetComponent<Rigidbody>().AddForce(direction);
    }
}
