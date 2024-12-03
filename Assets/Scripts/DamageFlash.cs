using UnityEngine;
using UnityEngine.UI; 
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    public Image damageVfx; 
    public AudioSource audioSource;  
    public AudioClip damageSound;    

    void Start()
    {
        damageVfx.color = new Color(1f, 1f, 1f, 0f);  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Snake"))
        {
            StartCoroutine(FlashDamageEffect());
        }
    }

    private IEnumerator FlashDamageEffect()
    {
        damageVfx.color = new Color(1f, 0f, 0f, 1f); 
        audioSource.PlayOneShot(damageSound);         
        yield return new WaitForSeconds(0.2f);       
        damageVfx.color = new Color(1f, 1f, 1f, 0f); 
    }
}