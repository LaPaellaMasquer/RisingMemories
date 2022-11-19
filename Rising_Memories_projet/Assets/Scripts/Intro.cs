using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    public VideoPlayer vplayer; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitvideo());
    }

    IEnumerator waitvideo()
    {
        yield return new WaitForSeconds((float)vplayer.clip.length);

        SceneManager.LoadScene("StartMenu");
    }
}
