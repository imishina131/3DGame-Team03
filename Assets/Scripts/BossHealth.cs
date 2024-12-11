using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class BossHealth : MonoBehaviour
{
    int health = 200;
    public Animator animator;
    public Slider healthBar;
    public ParticleSystem hit;
    public ParticleSystem smoke;

    public BossBehavior boss;
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
        hit.Play();
        if (health <= 0)
        {
            animator.SetTrigger("die");
            smoke.Play();
            boss.gameOver = true;
            StartCoroutine("LoadEnd");
        }
    }

    IEnumerator LoadEnd()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Cinematic 2");
    }
}
