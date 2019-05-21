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
    [SerializeField] Collider triggerBox;
    [SerializeField] AudioSource soundOnTrigger;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // checks if the player is putting a recipe on the board, if true: spawn the ingredients and disable the other recipes. also shortly disables the grab of the player so the recipe doesnt spasm out.
    void OnTriggerEnter(Collider c){
        if(Recipes.Contains(c.gameObject)){
            Ingredients.SetActive(true); 
            Recipetext.SetActive(false);
            Gametext.SetActive(true);
            soundOnTrigger.Play();
            foreach(GameObject rec in Recipes){
                if(c.gameObject == rec){
                    continue;
                };
                rec.SetActive(false);
            }
            ((OVRGrabber) FindObjectOfType(typeof(OVRGrabber))).enabled = false;
            ((OVRGrabber) FindObjectOfType(typeof(OVRGrabber))).enabled = true;
        }
    }

    //called when a collider enters the trigger zone
    //keeps the recipe in place against the board.
    void OnTriggerStay(Collider c){
        if(Recipes.Contains(c.gameObject)){
            //make sure the recipe is in the right place and no longer being grabbed by the player
            BeerGame.SelectRecipe(c.gameObject.GetComponent<Recipe>());
            c.attachedRigidbody.isKinematic = true; //recipe stuck to the wall
            c.gameObject.transform.eulerAngles = new Vector3(90,(float) -219.4,0);
            c.gameObject.transform.localPosition = new Vector3((float) -1.001, (float) 1.784, (float) 0.527);
            c.attachedRigidbody.freezeRotation = true;
            c.enabled = false;
        }  
    }


    // makes sure the recipe gets its original values back if it does exit the trigger zone. (shouldnt happen)
    void OnTriggerExit(Collider c){
        if(Recipes.Contains(c.gameObject)){
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
