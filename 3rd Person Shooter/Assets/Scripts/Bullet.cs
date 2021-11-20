using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    private void OnTriggerEnter(Collider other)
    {

        if ((other.gameObject.tag == "Red" || other.gameObject.tag == "White" || other.gameObject.tag == "Yellow") || other.gameObject.tag == "Stopper")
        {

            this.gameObject.SetActive(false);
            if (other.tag != "Stopper") {
            
                Destroy(other.gameObject); 
            
            }

        }
        
    }

}
