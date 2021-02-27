using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
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

    private void moveObstacle()
    {
        transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed);

    }
}
