using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static float health = 100f;
    static int bullets = 0;
    public static int keys = 0;
    static int healthMax;
    public TMP_Text bulletsText;
    public TMP_Text keysText;
    public TMP_Text cookiesText;
    public TMP_Text firePotionsText;
    public Slider healthBar;
    public int nonStaticBullets;
    Scene currentScene;
    int numberOfTargets = 3;
    public static int numberOfFirePotions = 0;
    public static int cookies = 0;
    public bool hasCookies;
    public bool hasPotions;
    public bool ghostGirlDead;

    public Image damageVfx; 
    public AudioSource audioSource;  
    public AudioClip damageSound; 
    public AudioClip keyPickup;
    bool dodge;
    public ParticleSystem explosionEffect;




    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        damageVfx.color = new Color(1f, 1f, 1f, 0f);

        if(TaskCompleted.riddleCompleted == true)
        {
            GetKey();
            audioSource.PlayOneShot(keyPickup);
        }

        if(currentScene.name == "FloorUnderVan")
        {
            bullets = 0;
        }

        if(currentScene.name == "Level01")
        {
            health = 100;
            keys = 0;
        }
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
        else
        {
            hasPotions = false;
        }

        if(keys > 0)
        {
            PlayerInteractions.hasKey = true;
        }
        else 
        {
            PlayerInteractions.hasKey = false;
        }
        bulletsText.text = bullets + "/5";
        keysText.text = keys + "/1";
        cookiesText.text = cookies + "/1";
        firePotionsText.text = numberOfFirePotions + "/4";

        if (health <= 0)
        {
            StartCoroutine(PlayerDeath());
        }

        if(numberOfTargets <= 0)
        {
            TaskCompleted.rangeCompleted = true;
            if(numberOfFirePotions == 0)
            {
                numberOfFirePotions += 4;
            }
            else if(numberOfFirePotions > 0)
            {
                numberOfFirePotions = 4;
            }

            if(cookies == 0)
            {
                cookies += 1;
            }
            else if(cookies > 0)
            {
                cookies = 1;
            }
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
        if(dodge == false)
        {
            health = health - damage;
            Debug.Log("Health: " + health);
            StartCoroutine(FlashDamageEffect());
        }
        else
        {
            return;
        }
    }

    private IEnumerator FlashDamageEffect()
    {
        damageVfx.color = new Color(1f, 0f, 0f, 1f); 
        audioSource.PlayOneShot(damageSound);         
        yield return new WaitForSeconds(0.2f);       
        damageVfx.color = new Color(1f, 1f, 1f, 0f); 
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

    }

    public void useCookie()
    {
        cookies = 0;

    }

    public void Roll()
    {
        dodge = true;
        Invoke("Standup", 2.0f);
    }

    void Standup()
    {
        dodge = false;
    }

    IEnumerator PlayerDeath()
    {
        if(currentScene.name == "FloorUnderVan")
        {
            SceneManager.LoadScene("Level02");
        }
        explosionEffect.Play();
        yield return new WaitForSeconds(1);
        if (currentScene.name == "Level01")
        {
            SceneManager.LoadScene("Level01");
            healthMax = 100;
            health = healthMax;
            keys = 0;
        }
        else if (currentScene.name == "Level02")
        {
            PlayerInteractions.posSaved = new Vector3(PlayerInteractions.beginningPos.x, PlayerInteractions.beginningPos.y, PlayerInteractions.beginningPos.z);
            SceneManager.LoadScene("Level02");
            health = 100;
            bullets = 0;
            cookies = 0;
            numberOfFirePotions = 0;

        }
        else
        {
            SceneManager.LoadScene("LossScene");
        }
    }
}
