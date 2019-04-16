using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerGame : MonoBehaviour
{

    [Header("Ingredients in the right order")]
    [SerializeField] List<GameObject> ingredients;
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
        nextIngredient = ingredients[0];
    }

    public bool addIngredient(GameObject i){
        if(nextIngredient == i){
            Yay.Play();
            if(ingredients.IndexOf(nextIngredient) == ingredients.Count -1){
                endGame();
                return true;
            }
            nextIngredient = ingredients[ingredients.IndexOf(nextIngredient) + 1];
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
