using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 mouseStartPosition;
    public float rotationSpeed;
    public float movementSpeed;
    public Brush brush;

    void Start()
    {
    }

    void Update()
    {


        if (GameManager.Instance.gameState==GameManager.gameStates.run && GameManager.Instance.playerState==GameManager.playerStates.onGround)
        {
            RunControl();
        }
        else if (GameManager.Instance.gameState == GameManager.gameStates.paint && GameManager.Instance.playerState == GameManager.playerStates.onGround)
        {
             PaintControl();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag=="finish")
        {
            gameObject.GetComponent<Animator>().SetBool("run", false);
            GameManager.Instance.gameState = GameManager.gameStates.paint;
        }
        else if (collision.transform.tag == "HorizontalObstacle")
        {
            gameObject.GetComponent<Animator>().SetBool("fall", true);
            gameObject.GetComponent<Animator>().SetBool("run", false);
        }
        else if (collision.transform.tag == "platform")
        {
            transform.parent.parent = null;
        }
        else if (collision.transform.tag == "rotatingPlatform")
        {
            transform.parent.parent = collision.gameObject.transform;
        }
        else if (collision.transform.tag == "rotatingStick")
        {

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
            gameObject.GetComponent<Animator>().SetBool("run", true);
        }
        if (Input.GetMouseButton(0))
        {
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
