using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddyDoll : MonoBehaviour
{
    public Player player;
    AudioSource audioSource;
    public AudioClip freddySpeech;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(PlayerInteractions.numberOfVisits < 1)
        {
            audioSource.clip = freddySpeech;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
