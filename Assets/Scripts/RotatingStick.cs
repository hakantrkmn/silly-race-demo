using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    public float force;
    public float forceYAxis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

            if (collision.transform.tag == "Player")
            {
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
