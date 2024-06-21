using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemigo : MonoBehaviour
{
    //NavMeshAgent agent;
    //PlayerInput target;
    private float timer = 0f;
    public Animator animator;
    private Transform player;
    private NavMeshAgent agent;

    [SerializeField] public float detectionRange = 0f;
    [SerializeField] public float attackRange = 0f;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //if (timer >= 0.5f) 
        //{
        //    agent.SetDestination(target.transform.position);
        //    timer = 0f;
        //}
        animator.SetFloat("Speed", 1);

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {           
                agent.isStopped = true;
                animator.SetBool("IsAttacking", true);
            }
            else
            {
                agent.isStopped = false;
                animator.SetBool("IsAttacking", false);
                animator.SetFloat("Speed", 1);
            }
        }
        else
        {

            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsAttacking", false);
        }

    }
}
