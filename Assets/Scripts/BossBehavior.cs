using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Animator animator;
    bool inAttackArea;
    bool onBreak;
    public GameObject playerObject;
    public Player player;
    public GameObject rollHint;
    public Transform initialPosition;

    public bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerObject.transform.position, transform.position);
        Debug.Log("distance boss: " + distance);
        if(PlayerInteractions.startBoss == true)
        {
            rollHint.SetActive(true);

            if(distance <= 30)
            {
                agent.SetDestination(playerObject.transform.position);
                animator.SetBool("far", true);
            }
            else if (distance > 30)
            {
                agent.SetDestination(initialPosition.position);
                animator.SetBool("far", true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossAttackArea"))
        {
            inAttackArea = true;
            animator.SetBool("far", false);
            StartCoroutine(Attack());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("BossAttackArea"))
        {
            inAttackArea = false;
            animator.SetBool("close", false);
        }
    }

    IEnumerator Attack()
    {
        while(inAttackArea && !gameOver)
        {
            animator.SetTrigger("attack");
            player.TakeDamage(25);
            yield return new WaitForSeconds(4f);
        }
    }

}
