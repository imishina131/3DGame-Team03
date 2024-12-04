using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGirlHealth : MonoBehaviour
{
    int health = 100;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        health = health - 25;

        if(health <= 0)
        {
            player.ghostGirlDead = true;
            Destroy(gameObject);
        }
    }
}
