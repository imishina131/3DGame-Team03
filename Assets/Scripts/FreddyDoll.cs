using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddyDoll : MonoBehaviour
{
    public Player player;
    AudioSource audioSource;
    public AudioClip freddySpeech;
    static int numberOfVisits = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(numberOfVisits < 1)
        {
            audioSource.clip = freddySpeech;
            audioSource.Play();
        }
        numberOfVisits += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
