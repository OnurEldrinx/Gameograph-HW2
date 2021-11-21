using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject rockPrefab;

    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("SpawnEnemy",2,1f);

    }

    // Update is called once per frame
    void Update()
    {
        
        

    }

    public void SpawnEnemy()
    {
        if(!PlayerController.PlayerInstance.isDead && !PlayerController.PlayerInstance.levelDone)
            Instantiate(rockPrefab,new Vector3(Random.Range(-2f,2f),1,90f),rockPrefab.transform.rotation);

    }
}
