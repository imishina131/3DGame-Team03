using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    int health = 200;
    public Animator animator;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
        Debug.Log("boss:" + health);
    }

    public void TakeDamage()
    {
        health = health - 40;

        if(health <= 0)
        {
            animator.SetTrigger("die");
            StartCoroutine("LoadEnd");
        }
    }

    IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Cinematic scene");
    }
}
