using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; 
    private static float volume = 1f; 

    private void Start()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume", 1f);

        if (volumeSlider != null)
        {
            volumeSlider.value = volume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChange); 
        }

        UpdateVolume(volume);
    }

    public void OnVolumeChange(float value)
    {
        volume = value;
        UpdateVolume(volume);

        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    private void UpdateVolume(float value)
    {
        GameObject audioObject = GameObject.FindGameObjectWithTag("GameMusic");
        if (audioObject != null)
        {
            AudioSource audioSource = audioObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.volume = value; 
            }
        }
        else
        {
            Debug.LogWarning("GameMusic object not found!");
        }
    }
}