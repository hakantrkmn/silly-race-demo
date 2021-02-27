using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;

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
        transform.Rotate(new Vector3(0, 1, 0), 5 * rotateSpeed);
    }
}
