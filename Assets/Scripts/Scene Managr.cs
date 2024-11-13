using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void GoToSettings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

     public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

     public void GoToHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu"); 
    }

     public void GoToAudioScene()
    {
        SceneManager.LoadScene("AudioMenu"); 
    }
}