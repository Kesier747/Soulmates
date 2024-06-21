using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    PlayerInput target;

    [SerializeField] private float droneHP;
    [SerializeField] private float droneDamage;
    [SerializeField] public float droneDamageReceived;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        target = FindObjectOfType<PlayerInput>();
    }


    void Update()
    {
        agent.SetDestination(target.transform.position);

    }

    public void ReceiveDamage(float damageRecibido)
    {
        droneHP -= damageRecibido;
        if (droneHP <= 0)
        {
            agent.enabled = false; 
            this.enabled = false; //el bicho pierde su alma (se queda sin script)
            Destroy(gameObject);
        }
    }
}
