using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WideRobot : MonoBehaviour
{
    public float detectionRange = 300f;
    public float attackRange = 15f;
    public Animator animator;
    private Transform player;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
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
                animator.SetFloat("Speed", agent.velocity.magnitude);
            }
        }
        else
        {
            
            agent.isStopped = true;
            animator.SetFloat("Speed", 0);
            animator.SetBool("IsAttacking", false);
        }
    }
    //NavMeshAgent agent;
    //PlayerInput target;

    [SerializeField] private float wideRobotHP;
    [SerializeField] private float wideRobotDamage;
    [SerializeField] public float wideRobotDamageReceived;

    // Start is called before the first frame update
    //void Start()
    //{
    //    agent = GetComponent<NavMeshAgent>();
    //    target = FindObjectOfType<PlayerInput>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    agent.SetDestination(target.transform.position);
    //    Debug.Log(wideRobotHP);

    //}
    public void ReceiveDamage(float damageRecibido)
    {
        wideRobotHP -= damageRecibido;
        if (wideRobotHP <= 0)
        {
            agent.enabled = false; //ya deja de seguir al player
            //anim.enabled = false; //ya no se mueve
            //CambiarEstadoHuesos(false); //adivina
            this.enabled = false; //el bicho pierde su alma (se queda sin script)
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    other.gameObject.GetComponent<WideRobot>().ReceiveDamage(pistolDamage);
        //    Destroy(gameObject);
        //}
    }
}
