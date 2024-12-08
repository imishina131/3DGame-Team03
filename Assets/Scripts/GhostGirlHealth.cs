using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostGirlHealth : MonoBehaviour
{
    int health = 100;
    public Player player;
    public GameObject potionsHint;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage()
    {
        health = health - 25;

        if(health <= 0)
        {
            player.ghostGirlDead = true;
            potionsHint.SetActive(false);
            Destroy(gameObject);
        }
    }
}
