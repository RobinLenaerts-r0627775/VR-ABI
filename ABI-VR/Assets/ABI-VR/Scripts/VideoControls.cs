using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TogglePlayPause()
    {
        var video = GetComponent<VideoPlayer>();
        if (video.isPlaying)
        {
            video.Pause();
        }
        else
        {
            video.Play();
        }
    }
}
