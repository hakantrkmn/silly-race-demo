﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spray : MonoBehaviour
{

    void Update()
    {
        if (GameManager.Instance.gameState==GameManager.gameStates.paint)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.transform.gameObject==transform.parent.gameObject)
                {
                    transform.position = Vector3.Lerp(transform.position,hitInfo.point,15*Time.deltaTime);
                }
            }
        }
    }
}
