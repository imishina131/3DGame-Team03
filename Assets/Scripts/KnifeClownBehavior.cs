using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeClownBehavior : MonoBehaviour
{
    public GameObject knife;
    GameObject knifeClone;
    GameObject firstKnife;
    public Transform knifePos;
    public Transform knifeEndPos;
    public Transform firstKnifePos;
    float speed = 1.0f;
    bool shootingFirstDoor;
    bool shootingSecondDoor;
    public Transform knifeTarget;

    AudioSource audioSource;
    public AudioClip throwingKnives;
    

    public void ShootFirstKnife()
    {
        audioSource = GetComponent<AudioSource>();
        shootingFirstDoor = true;
        Vector3 direction = (new Vector3(firstKnifePos.transform.position.x, firstKnifePos.transform.position.y, firstKnifePos.transform.position.z) - (knifePos.transform.position));
        firstKnife = Instantiate(knife, knifePos.position, Quaternion.identity);
        firstKnife.gameObject.transform.localScale = new Vector3(340f, 340f, 340f);
        firstKnife.transform.Rotate(-90f, 1.5f, -90f);
        firstKnife.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        audioSource.clip = throwingKnives;
        audioSource.Play();
        StartCoroutine("Shooting");
    }

    IEnumerator Shooting()
    {
        while(shootingFirstDoor == true)
        {
            yield return new WaitForSeconds(2.26f);
            Vector3 direction = (new Vector3(knifeEndPos.transform.position.x, knifeEndPos.transform.position.y, knifeEndPos.transform.position.z) - (knifePos.transform.position));

            knifeClone = Instantiate(knife, knifePos.transform.position, Quaternion.identity);

            knifeClone.gameObject.transform.localScale = new Vector3(340f, 340f, 340f);
            knifeClone.transform.Rotate(-90f, 1.5f, -90f);
            knifeClone.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            audioSource.clip = throwingKnives;
            audioSource.Play();
        }
        
    }

    public void ChangeTarget()
    {
        shootingFirstDoor = false;
        gameObject.transform.Rotate(0f, -90f, 0f);
        StartCoroutine("ShootingSecondDoor");
    }

    IEnumerator ShootingSecondDoor()
    {
        shootingSecondDoor = true;
        while(shootingSecondDoor == true)
        {
            yield return new WaitForSeconds(2.26f);
            Vector3 direction = (new Vector3(knifeTarget.transform.position.x, knifeTarget.transform.position.y, knifeTarget.transform.position.z) - (knifePos.transform.position));

            knifeClone = Instantiate(knife, knifePos.transform.position, Quaternion.identity);

            knifeClone.gameObject.transform.localScale = new Vector3(340f, 340f, 340f);
            knifeClone.transform.Rotate(-90f, -90f, -90f);
            knifeClone.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
        }
    }
}
