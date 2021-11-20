using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool pool;
    public List<GameObject> poolObjects;
    public GameObject bulletPrefab; // object to pool
    public int poolSize;

    private void Awake()
    {

        pool = this;

    }
    // Start is called before the first frame update
    void Start()
    {

        poolObjects = new List<GameObject>();
        GameObject temp;
        for(int i = 0; i < poolSize; i++)
        {

            temp = Instantiate(bulletPrefab);
            temp.SetActive(false);
            poolObjects.Add(temp);

        }

    }
    
    


    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject getObjectFromPool()
    {

        for (int i=0;i<poolSize;i++)
        {

            if (!poolObjects[i].activeInHierarchy)
            {

                return poolObjects[i];
                

            }

        }



        return null;
    }
}
