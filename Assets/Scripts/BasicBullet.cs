using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float pistolBulletStrengh;
    [SerializeField, Range(0, 100)] private int pistolDamage;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * pistolBulletStrengh, ForceMode.Impulse);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<WideRobot>().ReceiveDamage(pistolDamage);
            Destroy(gameObject);          
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
