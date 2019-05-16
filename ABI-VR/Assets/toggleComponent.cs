using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleComponent : MonoBehaviour
{

    [SerializeField] private GameObject toToggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle(){
        if(toToggle.active) toToggle.SetActive(false);
        else toToggle.SetActive(true);
    }
}
