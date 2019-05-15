using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImitateObjectPos : MonoBehaviour
{

    [SerializeField] private GameObject ImitateObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = ImitateObject.transform.localPosition;
        gameObject.transform.rotation = ImitateObject.transform.rotation;
    }
}
