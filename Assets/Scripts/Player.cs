using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    static int health = 100;
    public TMP_Text healthText;
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health: " + health;

        if(health <= 0)
        {
            if(currentScene.name == "Level01")
            {
                SceneManager.LoadScene("Level01");
                health = 100;
            }

            if(currentScene.name == "Level02")
            {
                SceneManager.LoadScene("Level02");
                health = 50;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log("Health: " + health);
    }
}
