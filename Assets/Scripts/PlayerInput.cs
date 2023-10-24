using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    Rigidbody rb;

    private float inputH;
    private float inputV;
    [SerializeField] private float speed;

    private Vector3 spawnPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();

        spawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(inputH, 0, inputV).normalized * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    //private void FixedUpdate()
    //{
    //    //ForceMode.Force: fuerza continua (FIXED UPDATE)
    //    //ForceMode.Impulse: fuerza INSTANTÁNEA.
    //    rb.AddForce(new Vector3(inputH, 0, inputV) * speed ,ForceMode.Impulse);
    //}
}
