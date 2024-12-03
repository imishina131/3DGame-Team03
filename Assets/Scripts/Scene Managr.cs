using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
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
    
    public void GoToVideoScene()
    {
        SceneManager.LoadScene("VideoMenu"); 
    }

    public void GoToLevel01()
    {
        SceneManager.LoadScene("TransitionScene");
    }

    public void GoToLevel02()
    {
        SceneManager.LoadScene("Level02");
    }

      public void GoToCreditsMenu()
    {
        SceneManager.LoadScene("CreditsMenu"); 
    }

    public void Quit()
    {
        Application.Quit();
    }
}