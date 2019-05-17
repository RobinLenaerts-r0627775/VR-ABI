using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // play audio when paused, pause video when played on click.
    public void TogglePlayPause()
    {
        var audio = GetComponent<AudioSource>();
        if (audio.isPlaying)
        {
            audio.Pause();
        }
        else
        {
            audio.Play();
        }
    }
}
