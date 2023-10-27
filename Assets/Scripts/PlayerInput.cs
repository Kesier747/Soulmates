using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    Rigidbody rb;

    private float inputH;
    private float inputV;
    [SerializeField] private float speed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float stamina;

    private Vector3 spawnPoint;

    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask whatIsGround;
    private float RayLength;


    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();

        spawnPoint = transform.position;

        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        transform.Translate(new Vector3(inputH, 0, inputV).normalized * speed * Time.deltaTime);

        Ray cameraRay = camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(cameraRay, out )) ;
        
    }

    //private void FixedUpdate()
    //{
    //    //ForceMode.Force: fuerza continua (FIXED UPDATE)
    //    //ForceMode.Impulse: fuerza INSTANTÁNEA.
    //    rb.AddForce(new Vector3(inputH, 0, inputV) * speed ,ForceMode.Impulse);
    //}
}
