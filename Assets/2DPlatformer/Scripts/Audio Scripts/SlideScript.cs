using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideScript : MonoBehaviour
{

    public AudioClip SlideSound;

    public AudioSource SlideSource;
    // Use this for initialization
    void Start()
    {
        SlideSource.clip = SlideSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            SlideSource.Play();
    }
}
