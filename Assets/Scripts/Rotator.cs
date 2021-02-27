using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : Obstacle
{

    private void Start()
    {
        obstaclesType = obstacles.Rotator;
    }
    void Update()
    {
        moveObstacle();
    }

}
