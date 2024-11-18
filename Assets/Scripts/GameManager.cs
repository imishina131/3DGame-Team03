using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadNextLevel");
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Level02");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
