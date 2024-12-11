using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int index;
    GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        
        music = GameObject.FindGameObjectWithTag("GameMusic");
        if(music.gameObject.TryGetComponent<DontDestroy>(out DontDestroy component))
        {
            index = component.index;
        }
        
        StartCoroutine("LoadNextLevel");
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(5);
        if(index == 1)
        {
            SceneManager.LoadScene("Level01");
        }
        else if(index == 2)
        {
            SceneManager.LoadScene("Level02");
        }
        else if(index == 3)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
