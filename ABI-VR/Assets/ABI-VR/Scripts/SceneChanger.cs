using UnityEngine;
using Interactive360;

public class SceneChanger : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    // finds the game manager object. 
    void Start()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    //calls the select scene method of the game manager.
    public void ChangeScene(string sceneName)
    {
        gm.SelectScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
