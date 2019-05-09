using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGame : MonoBehaviour
{

    [Header("Ingredients in the right order")]
    [SerializeField] List<Recipe> Recipes;
    private Recipe selectedRecipe;
    [SerializeField] GameObject container;
    [SerializeField] GameObject reward;

    [Header("Audio")]
    [SerializeField] AudioSource Boo;
    [SerializeField] AudioSource Yay;
    [SerializeField] ParticleSystem Particle;

    [Header("Particles")]
    GameObject nextIngredient;


    // Start is called before the first frame update
    void Start()
    {
        selectedRecipe = Recipes[0];
        List<Renderer> results = new List<Renderer>();
        container.GetComponentsInChildren(true, results);
    }

    public void SelectRecipe(Recipe Recipe){
        if (selectedRecipe != Recipe) selectedRecipe = Recipe;
    }

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

    public void endGame(){
        reward.SetActive(true);
        Particle.Play();
    }
}
