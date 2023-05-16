using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public UIManager uimanager;
    public void Start()
    {
        PointCalculator(0);
        Debug.Log(PlayerPrefs.GetInt("starpoint"));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("finish"))
        {
            Debug.Log("Oyun Bitti");
            PointCalculator(100);
            uimanager.pointupdate();
            Debug.Log(PlayerPrefs.GetInt("starpoint"));

        }
    }

    public void PointCalculator(int star)
    {
        if (PlayerPrefs.HasKey("starpoint"))
        {
            int oldscore = PlayerPrefs.GetInt("starpoint");
            PlayerPrefs.SetInt("starpoint", oldscore + star);
        }
        else
            PlayerPrefs.SetInt("starpoint", 0);
    }
}
