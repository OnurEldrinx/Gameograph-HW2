using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]private int health;
    [SerializeField] private int point;
    [SerializeField] private float speed = 2;
    [SerializeField] private GameObject[] destinations;
    private int destinationIndex;


    private NavMeshAgent enemyAgent;

    // Start is called before the first frame update
    void Start()
    {
        destinationIndex = 0;

        enemyAgent = GetComponent<NavMeshAgent>();

        GoToNext();

    }

    // Update is called once per frame
    void Update()
    {

        if (enemyAgent.remainingDistance < 0.5 && !enemyAgent.pathPending)
        {

            GoToNext();

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








}
