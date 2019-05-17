using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    [Header("Float parametres")]
    public float amplitude;          
    public float speed;                  
    private float tempVal;
    private Vector3 tempPos;


    //get initial position of the object.
    void Start()
    {
        tempVal = transform.position.y;
        tempPos = transform.position;
    }

    // make the height value update following a sine wave.
    void Update()
    {
        tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.position = tempPos;
    }
}
