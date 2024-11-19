using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    public int index;
    Scene scene;

    void Awake()
    {
        GameObject[] musicObjs; 
        musicObjs = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObjs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        scene = SceneManager.GetActiveScene();

        if(scene.name == "MainMenu")
        {
            index = 1;
        }
        if(scene.name == "Level01")
        {
            index = 2;
        }
        if(scene.name == "Level02")
        {
            index = 3;
        }
    }
}
