using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float lerpSpeed;
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
            GameManager.Instance.playerState = GameManager.playerStates.onGround;
            collision.gameObject.GetComponent<Animator>().SetBool("fall", false);
            var vector = collision.transform.rotation.eulerAngles;
            vector.x = transform.rotation.eulerAngles.z;
            collision.gameObject.transform.rotation = Quaternion.Euler(vector);
        }
    }
}
