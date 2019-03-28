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

    void Update()
    {
        tempPos.z = tempVal + amplitude * Mathf.Sin(speed * Time.time);
        transform.localScale = tempPos;
    }
}
