using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGame : MonoBehaviour
{
    [SerializeField] List<GameObject> ingredients;
    [SerializeField] GameObject container;
    GameObject nextIngredient;


    // Start is called before the first frame update
    void Start()
    {
        nextIngredient = ingredients[0];
    }

    public bool addIngredient(GameObject i){
        if(nextIngredient == i){
            if(ingredients.IndexOf(nextIngredient) == ingredients.Count -1){
                endGame();
                return true;
            }
            nextIngredient = ingredients[ingredients.IndexOf(nextIngredient) + 1];
            return true;
        }
        return false;
    }

    public void endGame(){
        Debug.Log("end game");
    }
}
