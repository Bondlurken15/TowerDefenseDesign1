using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Jumpscare : MonoBehaviour
{
    VideoPlayer jumpscarePlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        jumpscarePlayer = GetComponentInChildren<VideoPlayer>();
    }

    public void JumpScare()
    {
        jumpscarePlayer.Play();
    }
}
