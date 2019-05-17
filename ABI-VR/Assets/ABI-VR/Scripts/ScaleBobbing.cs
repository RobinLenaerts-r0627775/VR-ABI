using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBobbing : MonoBehaviour
{

    [Header("Bobbing parametres")]
    public float amplitude;          //Set in Inspector 
    public float speed;                  //Set in Inspector 
    private float tempVal;
    private Vector3 tempPos;

    void Start()
    {
        tempVal = transform.localScale.z;
        tempPos = transform.localScale;
    }


    // same idea as the float script, but used to change the scale of an object following a sine wave.
    void Update()
    {
        tempPos.z = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.localScale = tempPos;
    }
}
