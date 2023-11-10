using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] private float strengh;
    [SerializeField, Range(0, 100)] private int damage;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * strengh, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
