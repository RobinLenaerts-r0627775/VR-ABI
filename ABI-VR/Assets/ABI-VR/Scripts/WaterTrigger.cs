using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{

    [Header("Up and Down Trigger to keep track of the bottles orientation")]
    [SerializeField] private GameObject Up;
    [SerializeField] private GameObject Down;
    [SerializeField] private ParticleSystem ParticleSystem;
    [SerializeField] private GameObject Game;
    [SerializeField] private GameObject Water;
    [SerializeField] private GameObject Container;
    private bool pour;
    private int count = 0;

    
    // Start is called before the first frame update
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
    void Update()
    {
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
    void OnParticleCollision(GameObject other){
        if(other == Container){
            Debug.Log("trigger");
            if(count++ >= 300){
                count = 0;
                if(Game.GetComponent<BeerGame>().addIngredient(Water)){
                    ParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                }
            }
        }
    }
}