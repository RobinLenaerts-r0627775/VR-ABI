using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] List<GameObject> Recipes;
    [SerializeField] BeerGame BeerGame;
    [SerializeField] GameObject Ingredients;
    [SerializeField] GameObject Recipetext;
    [SerializeField] GameObject Gametext;
    

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
            Debug.Log(" parent = " + c.gameObject.GetComponentInParent<OVRGrabber>());
            //make sure the recipe is in the right place and no longer being grabbed by the player
            ((OVRGrabber) FindObjectOfType(typeof(OVRGrabber))).ForceRelease(c.gameObject.GetComponent<OVRGrabbable>());
            BeerGame.SelectRecipe(c.gameObject.GetComponent<Recipe>());
            c.attachedRigidbody.isKinematic = true; //recipe stuck to the wall
            c.gameObject.transform.eulerAngles = new Vector3(90,(float) -219.4,0);
            /* Vector3 newpos = c.gameObject.transform.position;
            newpos.x = (float) -1.001; 
            newpos.y = (float) 1.533;
            newpos.z = (float) 0.527;*/
            c.gameObject.transform.localPosition = new Vector3((float) -1.001, (float) 1.553, (float) 0.527);
            Debug.Log("pos is: " + c.gameObject.transform.localPosition + " ::::: " + c.gameObject.transform.position);
            c.attachedRigidbody.freezeRotation = true;
            foreach(GameObject rec in Recipes){
                if(c.gameObject == rec)continue;
                rec.SetActive(false);
            }
            Recipetext.SetActive(false);
            Gametext.SetActive(true);
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
