using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGame : MonoBehaviour
{

    [Header("Ingredients in the right order + parent object")]
    [SerializeField] List<Recipe> Recipes;
    [SerializeField] GameObject Ingredients;

    [Header("Rewards (same order as recipes)")]
    [SerializeField] List<GameObject> rewards;
    private Recipe selectedRecipe;
    [SerializeField] GameObject container;

    [Header("Audio")]
    [SerializeField] AudioSource Boo;
    [SerializeField] AudioSource Yay;
    [Header("Confetti particles")]
    [SerializeField] ParticleSystem Particle;

    GameObject nextIngredient;

    [Header("UI")]
    [SerializeField] GameObject Gametext;
    [SerializeField] GameObject Wintext;


    // Start is called before the first frame update
    // set first recipe as default, 
    void Start()
    {
        selectedRecipe = Recipes[0];
        List<Renderer> results = new List<Renderer>();
        container.GetComponentsInChildren(true, results);
    }

    // method to select a recipe so the script knows what ingredients to expect.
    public void SelectRecipe(Recipe Recipe){
        if (selectedRecipe != Recipe) selectedRecipe = Recipe;
    }

    // method that checks if the ingredient that was added is the right one and plays a corresponding sound. 
    // also checks if it is the last ingredient in the recipe and ends the game when it is.
    public bool addIngredient(GameObject i){
        GameObject res;
        res = selectedRecipe.addIngredient(i);
        if(res == null){
            Yay.Play();
            endGame();
            return true;
        }
        if(res != i){
            Yay.Play();
            return true;
        }
        Boo.Play();
        return false;
    }

    // method to handle the game end. disables or enables different ui and objects. activates the confetti particles and spawns the reward.
    public void endGame(){
        Debug.Log("disabling ingredients");
        Ingredients.SetActive(false);
        Debug.Log("disabling gametext");
        Gametext.SetActive(false);
        Wintext.SetActive(true);
        rewards[Recipes.IndexOf(selectedRecipe)].SetActive(true);
        Particle.Play();
    }
}
