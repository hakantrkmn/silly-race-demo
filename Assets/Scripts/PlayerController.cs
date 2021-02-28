﻿using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<GameObject, Collision> onPlayerCollisionEntered;
    public static Action<int> onRankChanged;

    public Vector3 mouseStartPosition;
    public float rotationSpeed;
    public float movementSpeed;
    public Brush brush;
    public Transform finish;

    public float EndDistance;
    public int line;
    public int PreRank;

    void Start()
    {
        PreRank = 10;
        InvokeRepeating("GetRank",0, 2);
    }

    void GetRank()
    {
        Ranking.Instance.Rank();
        if (line!=PreRank)
        {
            onRankChanged(line+1);
        }
    }

    void Update()
    {
        CalculateDistance();
        if (GameManager.Instance.gameState==GameManager.gameStates.run && GameManager.Instance.playerState==GameManager.playerStates.onGround)
        {
            RunControl();
        }
        else if (GameManager.Instance.gameState == GameManager.gameStates.paint && GameManager.Instance.playerState == GameManager.playerStates.onGround)
        {
             PaintControl();
        }
    }

    void CalculateDistance()
    {
        EndDistance = (finish.transform.position - transform.position).magnitude;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onPlayerCollisionEntered != null)
        {
            onPlayerCollisionEntered(gameObject, collision);
        }
        if (collision.transform.tag == "finish")
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
            GameManager.Instance.gameState = GameManager.gameStates.paint;
        }
        if (collision.transform.tag == "sea")
        {
            gameObject.transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation= Quaternion.Euler(0, 0, 0);
            gameObject.transform.position = Vector3.zero;
        }
    }


    private void PaintControl()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                if (paintObject != null)
                {
                    paintObject.Paint(brush, hitInfo);
                }
            }
        }
    }

    public void RunControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<Animator>().SetBool("run", true);
            transform.position += transform.forward * movementSpeed;
            var direction = Input.mousePosition - mouseStartPosition;
            if (mouseStartPosition != Input.mousePosition)
            {
                Quaternion toRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
        }
    }
}
