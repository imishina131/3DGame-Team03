using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DollBehavior : MonoBehaviour
{
    NavMeshAgent agent;
    public Animator animator;
    bool inAttackArea;
    bool onBreak;
    public Transform startPoint;

    public GameObject playerObject;
    public Player player;

    public GameObject jumpscare;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);
        Debug.Log("distance: " + distance);
        Debug.Log("attackarea: " + inAttackArea);

        if(distance <= 40 && !onBreak)
        {
            agent.SetDestination(playerObject.transform.position);
        }
        else if(distance > 40)
        {
            agent.SetDestination(startPoint.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "AttackArea")
        {
            inAttackArea = true;
            onBreak = true;
            StartCoroutine(Attack());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "AttackArea")
        {
            inAttackArea = false;
        }
    }

    IEnumerator Attack()
    {
        if(inAttackArea)
        {
            jumpscare.SetActive(true);
            Invoke("TakeOffJumpscare", 0.5f);
            player.TakeDamage(30);
            //animation
            yield return new WaitForSeconds(3);
            onBreak = false;
        }
    }

    void TakeOffJumpscare()
    {
        jumpscare.SetActive(false);
    }
}
