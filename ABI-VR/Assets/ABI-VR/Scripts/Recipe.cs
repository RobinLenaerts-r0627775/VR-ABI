using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{

    [SerializeField] private List<GameObject> ingredients;
    private GameObject next;

    // Start is called before the first frame update
    // initialize first ingredient as next to add.
    void Start()
    {
        next = ingredients[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns the expected ingredient.
    public GameObject nextIngredient(){
        return next;
    }

    //method to check wether the added ingredient is the right one.
    // returns null if it is the last ingredient
    // returns the given ingredient if it is not the right one
    //returns the next ingredient wanted if it is the right one. 
    public GameObject addIngredient(GameObject ingredient){
        if(next == ingredient){
            
            if(ingredients.IndexOf(next) == ingredients.Count -1){
                return null;
            }
            next = ingredients[ingredients.IndexOf(next) + 1];
            return next;
        }
        return ingredient;
    }
}
