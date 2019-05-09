using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{

    [SerializeField] private List<GameObject> ingredients;
    private GameObject next;

    // Start is called before the first frame update
    void Start()
    {
        next = ingredients[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject nextIngredient(){
        return next;
    }

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
