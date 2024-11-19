using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour
{
    public GameObject audio;
    AudioSource audioSource;

    Scene activeScene;
    public AudioClip level01Music;
    public AudioClip level02Music;
    public AudioClip loadingMusic;
    public AudioClip menuMusic;
    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
        audio = GameObject.FindGameObjectWithTag("GameMusic");
        audioSource = audio.GetComponent<AudioSource>();

        if(activeScene.name == "Level01")
        {
            audioSource.clip = level01Music;
        }
        else if(activeScene.name == "Level02")
        {
            audioSource.clip = level02Music;
        }
        else if(activeScene.name == "TransitionScene")
        {
            audioSource.clip = loadingMusic;
        }
        else if(activeScene.name == "MainMenu")
        {
            audioSource.clip = menuMusic;
        }

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
