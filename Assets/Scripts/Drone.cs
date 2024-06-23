using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    //UnityEngine.AI.NavMeshAgent agent;
    //PlayerInput target;

    [SerializeField] private float droneHP;
    [SerializeField] private float droneDamage;
    [SerializeField] public float droneDamageReceived;

    private Transform player;
    private NavMeshAgent agent;

    public float detectionRange = 300f;
    public float attackRange = 15f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //target = FindObjectOfType<PlayerInput>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);
        //Debug.Log(distanceToPlayer);

        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(player.position);
            agent.isStopped = false;

        }
        else
        {
            agent.isStopped = true;
        }

    }

    public void ReceiveDamage(float damageRecibido)
    {
        droneHP -= damageRecibido;
        if (droneHP <= 0)
        {
            agent.enabled = false; 
            this.enabled = false; //el bicho pierde su alma (se queda sin script)
            FindObjectOfType<AudioManager>().Play("RobotDeath");
            Destroy(gameObject);
        }
    }
}
