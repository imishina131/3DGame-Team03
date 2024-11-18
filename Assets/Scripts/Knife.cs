using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    float timer;
    public Player player;
    public GameObject door02;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        door02 = GameObject.FindGameObjectWithTag("Door02");
        timer += Time.deltaTime;

        if(timer > 2)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent<Player>(out Player playerComponent))
        {
            Destroy(gameObject);
            playerComponent.TakeDamage(10);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Target"))
        {
            Destroy(door02);
        }
    }
}
