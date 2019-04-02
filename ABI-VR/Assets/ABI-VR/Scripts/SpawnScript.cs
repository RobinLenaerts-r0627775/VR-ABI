using Interactive360;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = (GameManager)FindObjectOfType(typeof(GameManager));
        gm.SelectScene("TutorialScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
