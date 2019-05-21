using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTriggerReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    method to reset an object that falls on the floor
     */
    void OnTriggerStay(Collider other){

        if(other.tag == "GameController"){
            return;
        }
        Vector3 vec = other.gameObject.transform.localPosition;
        vec.x = (float) 0.204;
        vec.y = (float) 1.8;
        vec.z = (float) 0.461;
        other.gameObject.transform.localPosition = vec;
        other.attachedRigidbody.velocity = Vector3.zero;
        other.attachedRigidbody.angularVelocity = Vector3.zero;
    }
}