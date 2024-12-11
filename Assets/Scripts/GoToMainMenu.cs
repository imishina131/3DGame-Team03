using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadMainMenu());
    }
    
    IEnumerator LoadMainMenu()
    {
        yield return new WaitForSeconds(25);
        SceneManager.LoadScene("MainMenu");
    
    }
}
