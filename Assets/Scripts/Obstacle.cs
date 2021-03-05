using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Obstacle : MonoBehaviour
{
    public enum obstacles { Horizontal, RotatingPlatform, Rotator, RotatingStick,DonutStick };
    public obstacles obstaclesType;
    [Header("HorizontalObstacle and Donut Stick Settings")]
    public Vector3 PointOne;
    protected Vector3 PointTwo;
    public float speed;
    [Header("Donut Stick Settings")]
    public float timeInterval;
    [Header("Force Object Settings")]
    public float force;
    public float forceYAxis;
    [Header("Rotation Settings")]
    public float rotateSpeed;


    protected float timer;
    protected bool wait;
    protected Vector3 pointThree;

    //opponentlerin sınıfı. bu fonksiyonda her opponentin hareket fonksiyonunu yazıyoruz.ardından her alt objede objeyi belirtip move fonksiyonunu çalıştırmamız yetiyor
    public void moveObstacle()
    {
        if (obstaclesType == obstacles.Horizontal)
        {
            //pointone değerine 1 nokta belirliyoruz.Belirlediğimiz bu değer ile ilk durduğu pozisyonu arasında gidip geliyor.
            //pointTwo ilk başladığı pozisyona eşit. eğer obstacle pointOne noktasına varırsa pointTwo ile yerlerini değiştiyoruz
            //bu sayede cisim sürekli gidip geliyor.
            if (gameObject.transform.position == PointOne)
            {
                var temp = PointOne;
                PointOne = PointTwo;
                PointTwo = temp;
            }
            transform.position = Vector3.MoveTowards(transform.position, PointOne, speed*Time.deltaTime);
        }
        else if (obstaclesType == obstacles.RotatingPlatform)
        {
            transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed);
        }
        else if (obstaclesType == obstacles.Rotator)
        {
            transform.Rotate(new Vector3(0, 1, 0), 5 * rotateSpeed);
        }
        else if (obstaclesType == obstacles.DonutStick)
        {
            //horizantalObstacle daki aynı mantık.Point there objenin ilk bulunduğu konumu tutuyor.Bu sayede obje dönüşünü yaptığında wait bool unu true yaparak beklemesini sağlıyoruz.
            if (gameObject.transform.localPosition == PointOne)
            {
                var temp = PointOne;
                PointOne = PointTwo;
                PointTwo = temp;
                if (PointTwo==pointThree)
                {
                    wait = true;
                }
                else
                {
                    wait=false;
                }
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, PointOne, speed * Time.deltaTime);
        }
    }

    //eğer obstacle da force kullanacaksak bu fonksiyonu çağırıp objenin kendisini ve çarpışma collisionunu yollarsak gerçekleşiyor.
    //kuvvet çapan obje ile çarpma noktası çıkartılarak yön bulunuyor. Ardından çarpan objeye kuvvet uygulanıyor.
    public void ForceObject(Collision collision, GameObject gameObj)
    {
        var direction = (collision.contacts[0].point - collision.gameObject.transform.position).normalized;
        direction = (direction * force).normalized;
        direction = new Vector3(direction.x * 2, forceYAxis, direction.z * 2);
        gameObj.GetComponent<Rigidbody>().AddForce(direction * force,ForceMode.Impulse);
    }

}
