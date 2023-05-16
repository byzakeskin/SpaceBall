using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //Destroy bir objeyi tamamen siler(oyunda anl�k kasmalara yol a�ar)
    //Setactive objeyi g�r�nmez hale getirir
    public void OnCollisionEnter(Collision hit)
    {
      if (hit.gameObject.CompareTag("Untagged") || hit.gameObject.CompareTag("enemy")) // || veya, && ve
        {
            gameObject.SetActive(false);
        }  
    }
}
