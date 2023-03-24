using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotallyNotScarySound : MonoBehaviour
{

    public AudioSource source;
    public AudioClip notscary;

    public void Onclick()
    {
        source.PlayOneShot(notscary);
    }
}

