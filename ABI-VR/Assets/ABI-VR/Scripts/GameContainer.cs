using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour
{
    private BeerGame gm;

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

        if(other.tag == "GameController"){
            return;
        }

        if(bg.addIngredient(other.gameObject)){
            other.gameObject.SetActive(false);
        } else {
            Vector3 vec = other.gameObject.transform.localPosition;
            vec.x = (float) 0.204;
            vec.y = (float) 1.8;
            vec.z = (float) 0.461;
            other.gameObject.transform.localPosition = vec;
        }
    }

    //called when a collider exits the trigger zone
    void OnTriggerExit(Collider c){
        Debug.Log("Leave");
    }

    void OnParticleTrigger(){
        Debug.Log("contrainertrigger");
    }
}
