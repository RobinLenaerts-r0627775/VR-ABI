using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglePopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //method to toggle the popup part of the object on or off depending on its active state.
    public void Toggle()
    {
        if (gameObject.active)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
