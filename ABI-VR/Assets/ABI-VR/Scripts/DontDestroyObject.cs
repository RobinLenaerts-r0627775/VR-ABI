using UnityEngine;

//Add this script to any game object in your starting scene that you want to remain in all other scenes loaded
public class DontDestroyObject : MonoBehaviour
{


    //added to objects that need to stay when transitioning between scenes, like the camera rig, controllers,...
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
