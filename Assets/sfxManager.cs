using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfxManager : Singleton<sfxManager>
{
    public AudioClip chickenDie;
    public AudioClip bomb;
    public AudioClip layEgg;

    private AudioSource audio;
    public void play(AudioClip clip)
    {
        audio.PlayOneShot(clip);
    }
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
