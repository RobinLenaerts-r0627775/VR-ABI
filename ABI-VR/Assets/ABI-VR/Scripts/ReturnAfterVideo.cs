using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ReturnAfterVideo : MonoBehaviour
{

    [SerializeField] private VideoPlayer video;
    [SerializeField] private SceneChanger SceneChanger;
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

    //method to handle the looppointreached event
    //jumps to a given scene when the video reaches its end point.
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        SceneChanger.ChangeScene(JumpTo);
    }
}
