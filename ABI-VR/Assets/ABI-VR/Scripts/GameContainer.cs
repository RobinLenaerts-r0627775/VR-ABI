using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour
{

    BeerGame bg;
    // Start is called before the first frame update
    void Start(){
        bg = (BeerGame)FindObjectOfType(typeof(BeerGame));
    }

    // Called when a collider enters the trigger zone
    void OnTriggerEnter(Collider c){
        Debug.Log("Enter");
    }

    // called every time there is a collider in the trigger zone
    void OnTriggerStay(Collider other){
        Debug.Log("Stay");
        Debug.Log(other.name);

        if(bg.tag == "GameController"){
            return;
        }

        if(bg.addIngredient(other.gameObject)){
            other.gameObject.SetActive(false);
        } else {
            Vector3 vec = other.gameObject.transform.localPosition;
            vec.x = 0;
            vec.y = (float) 1.8;
            vec.z = 1;
            other.gameObject.transform.localPosition = vec;
        }
    }

    //called when a collider exits the trigger zone
    void OnTriggerExit(Collider c){
        Debug.Log("Leave");
    }
}
