using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class WaterTrigger : MonoBehaviour
{

    [Header("Up and Down Trigger to keep track of the bottles orientation")]
    [SerializeField] private GameObject Up;
    [SerializeField] private GameObject Down;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private GameObject Game;
    [SerializeField] private GameObject Water;
    [SerializeField] private GameObject Container;
    private ParticleSystem.MainModule pMain;
    private bool pour;
    private int count = 0;
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
    
    // Start is called before the first frame update
    // checks if the bottle is tilted and if water should flow.
    void Start()
    {
        if (Up.transform.position.y >= Down.transform.position.y){
            pour = false;
            ParticleSystem.Pause();
        }
        else{
            pour = true;
            ParticleSystem.Play();
        }

    }

    // Update is called once per frame
    // checks the bottle orientation and updates the pour boolian when needed.
    void Update()
    {
        pMain = ParticleSystem.main;
        pMain.startSize = Down.transform.position.y - Up.transform.position.y;
        if(pour){
            if (Up.transform.position.y >= Down.transform.position.y){
                pour = false;
                ParticleSystem.Stop();
            }
        }
        else{
            if (Up.transform.position.y < Down.transform.position.y){
                pour = true;
                ParticleSystem.Play();
            }
        }
    }

    //checks if the particles collide with a container. if they do, up the counter and if the counter reaches 300, reset it, stop the liquid particles from flowing and add the given ingredient to the selected recipe.
    void OnParticleCollision(GameObject other){
        if(other == Container){
            Debug.Log("collision with container");
            if(count++ >= 300){
                count = 0;
                if(Game.GetComponent<BeerGame>().addIngredient(Water)){
                    ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
    }
}