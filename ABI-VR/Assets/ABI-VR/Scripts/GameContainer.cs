using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour
{
    private BeerGame gm;
    // Start is called before the first frame update
    void Start(){
        gm = (BeerGame)FindObjectOfType(typeof(BeerGame));
    }

    // Called when a collider enters the trigger zone
    void OnTriggerEnter(Collider c){
        Debug.Log("Enter");
    }

    // called every time there is a collider in the trigger zone
    void OnTriggerStay(Collider c){
        Debug.Log("Stay");
        Vector3 pos = c.gameObject.transform.localPosition;
        pos.x = 1;
        pos.y = 2;
        pos.z = (float) -0.5;
        c.gameObject.transform.localPosition = pos;


    }

    //called when a collider exits the trigger zone
    void OnTriggerExit(Collider c){
        Debug.Log("Leave");
    }
}
