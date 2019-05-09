using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGame : MonoBehaviour
{

    [Header("Ingredients in the right order")]
    [SerializeField] List<Recipe> Recipes;

    [Header("Rewards (same order as recipes)")]
    [SerializeField] List<GameObject> rewards;
    private Recipe selectedRecipe;
    [SerializeField] GameObject container;

    [Header("Audio")]
    [SerializeField] AudioSource Boo;
    [SerializeField] AudioSource Yay;
    [SerializeField] ParticleSystem Particle;

    [Header("Particles")]
    GameObject nextIngredient;

    [Header("UI")]
    [SerializeField] GameObject Gametext;
    [SerializeField] GameObject Wintext;


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
        Gametext.SetActive(false);
        Wintext.SetActive(true);
        rewards[Recipes.IndexOf(selectedRecipe)].SetActive(true);
        Particle.Play();
    }
}
