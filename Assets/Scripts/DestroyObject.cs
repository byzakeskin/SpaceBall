using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //Destroy bir objeyi tamamen siler(oyunda anlýk kasmalara yol açar)
    //Setactive objeyi görünmez hale getirir
    public void OnCollisionEnter(Collision hit)
    {
      if (hit.gameObject.CompareTag("Untagged") || hit.gameObject.CompareTag("enemy")) // || veya, && ve
        {
            gameObject.SetActive(false);
        }  
    }
}
