using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] List<GameObject> Recipes;
    [SerializeField] BeerGame BeerGame;
    [SerializeField] GameObject Ingredients;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    //called when a collider enters the trigger zone
    void OnTriggerEnter(Collider c){
        if(Recipes.Contains(c.gameObject)){
            Debug.Log("Enter board");
            // enable the ingredients
            Ingredients.SetActive(true); 
            //make sure the recipe is in the right place and no longer being grabbed by the player
            c.gameObject.GetComponent<OVRGrabbable>().enabled = false;
            BeerGame.SelectRecipe(c.gameObject.GetComponent<Recipe>());
            c.attachedRigidbody.isKinematic = true; //recipe stuck to the wall
            c.gameObject.transform.eulerAngles = new Vector3(90,90,0);
            Vector3 newpos = c.gameObject.transform.position;
            newpos.x = (float) -1.19; 
            c.gameObject.transform.position = newpos;
            c.attachedRigidbody.freezeRotation = true;
            foreach(GameObject rec in Recipes){
                if(c.gameObject == rec)continue;
                rec.SetActive(false);
            }
        }  
    }

    void OnTriggerExit(Collider c){
        if(Recipes.Contains(c.gameObject)){
            Debug.Log("exit board");
            c.gameObject.GetComponent<OVRGrabbable>().enabled = true;
            c.attachedRigidbody.isKinematic = false;
            c.attachedRigidbody.freezeRotation = false;
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
