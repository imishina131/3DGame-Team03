using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    static float health = 100f;
    static int bullets = 0;
    public TMP_Text healthText;
    public TMP_Text bulletsText;
    public Image healthBar;
    public int nonStaticBullets;
    Scene currentScene;
    int numberOfTargets = 3;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = " " + health;
        bulletsText.text = "Ammo: " + bullets;

        if(health <= 0)
        {
            if(currentScene.name == "Level01")
            {
                SceneManager.LoadScene("Level01");
                health = 100;
            }
            else if(currentScene.name == "Level02")
            {
                SceneManager.LoadScene("Level02");
                health = 50;
            }
            else
            {
                SceneManager.LoadScene("LossScene");
            }
        }

        if(numberOfTargets <= 0)
        {
            SceneManager.LoadScene("WinScene");
        }
        else if(currentScene.name == "ShootingRange" && bullets <= 0)
        {
            SceneManager.LoadScene("LossScene");
        }

        nonStaticBullets = bullets;
    }

    public void AddBullet()
    {
        bullets += 1;
    }

    public void LoseBullet()
    {
        bullets -= 1;
    }

    public void DestroyTarget()
    {
        numberOfTargets -= 1;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log("Health: " + health);
        healthBar.fillAmount = health / 100f;
    }
}
