using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
   [Header("Wobble parametres")]
    public float amplitude;          
    public float speed;                  
    private float temprotz;
    private Vector3 temprot;


    //get initial position of the object.
    void Start()
    {
        temprotz = transform.eulerAngles.z;
        temprot = transform.eulerAngles;
    }

    // make the height value update following a sine wave.
    void Update()
    {
        temprot.z = temprotz + amplitude * Mathf.Sin(speed * Time.time);
        transform.eulerAngles = temprot;
    }
}
