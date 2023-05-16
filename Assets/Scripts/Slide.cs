using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    public float speed;

    void FixedUpdate()
    {
        gameObject.GetComponent<Transform>().localPosition = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));  //zaman deðerine baðlý olarak hareket eder
    }
}
