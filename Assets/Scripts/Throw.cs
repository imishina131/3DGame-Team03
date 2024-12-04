using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    Rigidbody rb;

    public float throwSpeed;

    public GameObject Burst;

    public GhostGirlHealth ghostGirlHealth;
    public GameObject ghostGirl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * throwSpeed;
        ghostGirl = GameObject.FindGameObjectWithTag("GhostGirl");
        ghostGirlHealth = ghostGirl.GetComponent<GhostGirlHealth>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Instantiate(Burst, transform.position, transform.rotation);

        if(collision.transform.tag == "GhostGirl")
        {
            ghostGirlHealth.TakeDamage();
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
