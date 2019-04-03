using Interactive360;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{

    GameObject voiceoverControl;
    MenuManager mm;
    // Start is called before the first frame update
    void Start()
    {
        voiceoverControl = GameObject.Find("/VoiceOverControl");
        mm = (MenuManager)FindObjectOfType(typeof(MenuManager));
        mm.addMenuScreen(voiceoverControl);
        voiceoverControl.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        mm.popMenuScreen();
        Destroy(voiceoverControl);
    }
}
