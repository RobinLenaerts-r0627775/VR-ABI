using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInstance : MonoBehaviour
{

    public static StaticInstance instance = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
