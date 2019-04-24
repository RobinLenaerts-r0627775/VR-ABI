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
        List<Renderer> results = new List<Renderer>();
        container.GetComponentsInChildren(true, results);
        foreach(Renderer child in results){
            child.material.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            GL.PushMatrix();
            GL.LoadOrtho();

            // activate the first shader pass (in this case we know it is the only pass)
            child.material.SetPass(0);
            // draw a quad over whole screen
            GL.Begin(GL.QUADS);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(1, 1, 0);
            GL.Vertex3(0, 1, 0);
            GL.End();
        }

        GL.PopMatrix();
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
