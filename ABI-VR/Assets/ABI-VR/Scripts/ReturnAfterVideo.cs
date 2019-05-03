using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ReturnAfterVideo : MonoBehaviour
{

    [SerializeField] private VideoPlayer video;
    [SerializeField] private string JumpTo;
    // Start is called before the first frame update
    void Start()
    {
        video.loopPointReached += EndReached;
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneChanger sc = new SceneChanger();
        sc.ChangeScene(JumpTo);
    }
}
