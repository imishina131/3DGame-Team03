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
    static int keys = 0;
    static int healthMax;
    public TMP_Text bulletsText;
    public TMP_Text keysText;
    public TMP_Text cookiesText;
    public TMP_Text firePotionsText;
    public Slider healthBar;
    public int nonStaticBullets;
    Scene currentScene;
    int numberOfTargets = 3;
    static int numberOfFirePotions = 0;
    static int cookies = 0;
    public bool hasCookies;
    public bool hasPotions;
    public bool ghostGirlDead;



    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(cookies > 0)
        {
            hasCookies = true;
        }
        else
        {
            hasCookies = false;
        }

        if(numberOfFirePotions > 0)
        {
            hasPotions = true;
        }
        bulletsText.text = bullets + "/5";
        keysText.text = keys + "/1";
        cookiesText.text = cookies + "/1";
        firePotionsText.text = numberOfFirePotions + "/5";

        if (health <= 0)
        {
            if(currentScene.name == "Level01")
            {
                SceneManager.LoadScene("Level01");
                healthMax = 100;
                health = healthMax;
            }
            else if(currentScene.name == "Level02")
            {
                SceneManager.LoadScene("Level02");
                health = 70;
            }
            else
            {
                SceneManager.LoadScene("LossScene");
            }
        }

        if(numberOfTargets <= 0)
        {
            numberOfFirePotions += 5;
            cookies += 1;
            SceneManager.LoadScene("Level02");
        }
        else if(currentScene.name == "ShootingRange" && bullets <= 0)
        {
            SceneManager.LoadScene("Level02");
        }

        healthBar.value = health;
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

    public void GetKey()
    {
        keys += 1;
    }

    public void UseKey()
    {
        keys -= 1;
    }

    public void TakeDamage(int damage)
    {
        health = health - damage;
        Debug.Log("Health: " + health);
    }

    public void GainHealth()
    {
        if(health <= 80)
        {
            health = health + 20;
        }
        else if(health > 80)
        {
            health = 100;
        }
    }

    public void usePotion()
    {
        numberOfFirePotions -= 1;

        if(numberOfFirePotions == 0)
        {
            hasPotions = false;
        }
    }

}
