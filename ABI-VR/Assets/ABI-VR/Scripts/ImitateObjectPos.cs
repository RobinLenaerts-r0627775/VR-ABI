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
    // make the attached object imitate the position of the given imitateObject. used for the home button on the hand model.
    void Update()
    {
        gameObject.transform.localPosition = ImitateObject.transform.localPosition;
        gameObject.transform.rotation = ImitateObject.transform.rotation;
    }
}
