using UnityEngine;
using Interactive360;

public class SceneChanger : MonoBehaviour
{
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = (GameManager)FindObjectOfType(typeof(GameManager));
    }

    public void ChangeScene(string sceneName)
    {
        gm.SelectScene(sceneName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
