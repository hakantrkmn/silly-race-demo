using Es.InkPainter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<GameObject, Collision> onPlayerCollisionEntered;
    public static Action<int> onRankChanged;

    Vector3 mouseStartPosition;
    public float rotationSpeed;
    public float movementSpeed;
    public Brush brush;
    public Transform finish;

    public float EndDistance;
    public int line;
    int PreRank;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        PreRank = 10;
        InvokeRepeating("GetRank",0, 1);
    }
    //sıralamayı alıp eğer öncekinden farkıysa actionu harekete geçiriyoruz.
    void GetRank()
    {
        Ranking.Instance.Rank();
        if (line!=PreRank)
        {
            onRankChanged(line+1);
            PreRank = line;
        }
    }

    void Update()
    {
        //sıralamayı hesaplayabilmek için bitişe kalan uzaklığı hesaplıyoruz
        CalculateDistance();
        //oyun durumuna göre oyun mekaniğini değiştiriyoruz.
        if (GameManager.Instance.gameState==GameManager.gameStates.run && GameManager.Instance.playerState==GameManager.playerStates.onGround && !animator.GetBool("fall"))
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

    //player herhangi bir collisiona girdiğinde actionu harekete geçiriyoruz
    private void OnCollisionEnter(Collision collision)
    {
        if (onPlayerCollisionEntered != null)
        {
            onPlayerCollisionEntered(gameObject, collision);
        }
        if (collision.transform.tag == "finish")
        {
            animator.SetBool("run", false);
            GameManager.Instance.gameState = GameManager.gameStates.paint;
        }
        if (collision.transform.tag == "sea")
        {
            gameObject.transform.parent.transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation= Quaternion.Euler(0, 0, 0);
            gameObject.transform.position = Vector3.zero;
        }
    }

    //playerin boyama mekaniği
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
    //playerin swerve mekaniği
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
            animator.SetBool("run", false);
        }
    }

    //animasyona eklediğimiz event sayesinde fonksiyonu çalıştırıyoruz.
    public void restartPlayer()
    {
        animator.SetBool("fallEnd", true);
        GameManager.Instance.playerState = GameManager.playerStates.onGround;
        animator.SetBool("fall", false);
        transform.position = Vector3.zero;

    }
}
