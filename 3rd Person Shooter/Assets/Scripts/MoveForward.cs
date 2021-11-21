using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if(!PlayerController.PlayerInstance.levelDone && !PlayerController.PlayerInstance.isDead)
            rb.AddForce(-Vector3.forward * 10);

        if (transform.position.z <= -10)
        {

            Destroy(gameObject);

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

            other.GetComponent<PlayerController>().isDead = true;
            other.GetComponentInChildren<Animator>().SetBool("isDead",true);

        }else if (other.tag == "Bullet")
        {

            Destroy(gameObject);
            PlayerController.PlayerInstance.score += 5;
            other.gameObject.SetActive(false);

        }

        

    }
}
