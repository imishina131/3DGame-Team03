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
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInteractions.startBoss == true)
        {
            animator.SetTrigger("start");
            agent.SetDestination(playerObject.transform.position);
            rollHint.SetActive(true);
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
        animator.SetTrigger("attack");
        player.TakeDamage(25);
        yield return new WaitForSeconds(4);
        onBreak = false;

    }
}
