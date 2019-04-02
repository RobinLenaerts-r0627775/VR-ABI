using Interactive360;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject voiceoverControl = GameObject.Find("/VoiceOverControl");
        MenuManager mm = (MenuManager)FindObjectOfType(typeof(MenuManager));
        mm.addMenuScreen(voiceoverControl);
    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
