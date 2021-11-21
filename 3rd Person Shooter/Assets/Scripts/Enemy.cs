using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int point;
    [SerializeField] private float damage = 5;
    [SerializeField] private GameObject[] destinations;
    private int destinationIndex;

    private Animator animator;


    private NavMeshAgent enemyAgent;

    private GameObject player;

    public bool playerDetected;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        if (GetComponent<Animator>()!=null)
        {

            animator = GetComponent<Animator>();


        }

        destinationIndex = 0;

        enemyAgent = GetComponent<NavMeshAgent>();

        GoToNext();

    }

    // Update is called once per frame
    void Update()
    {


        if (enemyAgent.remainingDistance < 0.5 && !enemyAgent.pathPending && !player.GetComponent<PlayerController>().isDead && !player.GetComponent<PlayerController>().levelDone && !playerDetected)
        {

            GoToNext();

        }

        if (enemyAgent.velocity.magnitude > 0 && !player.GetComponent<PlayerController>().isDead && !player.GetComponent<PlayerController>().levelDone)
        {

            animator.SetBool("isWalk", true);


        }
        else
        {

            animator.SetBool("isWalk", false);


        }

    }

    void GoToNext()
    {

        if (destinations != null)
        {

            enemyAgent.destination = destinations[destinationIndex].transform.position;

            

            destinationIndex = (destinationIndex + 1) % destinations.Length;

        }
        

        
        
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Player" && !other.GetComponent<PlayerController>().isDead && !player.GetComponent<PlayerController>().levelDone)
        {

            playerDetected = true;
            enemyAgent.destination = other.transform.position;
            animator.SetBool("isAttacking",true);
            other.gameObject.GetComponent<PlayerController>().health -= damage * Time.deltaTime;

        }
        else {

            animator.SetBool("isAttacking", false);


        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bullet")
        {

            animator.SetBool("isDead", true);
            StartCoroutine(killTimer(0.5f,gameObject));
            PlayerController.PlayerInstance.score += point;
            other.gameObject.SetActive(false);
        }

    }

    

    IEnumerator killTimer(float waitTime, GameObject enemy)
    {


        yield return new WaitForSeconds(waitTime);
        Destroy(enemy);
        

    }
















}
