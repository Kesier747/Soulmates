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
    [SerializeField] private GameObject bolaPrefab;

    private Vector3 spawnPoint;

    private Camera mainCamera;
    [SerializeField] private LayerMask whatIsGround;
    private float RayLength;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        spawnPoint = transform.position;

        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
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

        controller.Move(new Vector3(inputH, 0, inputV).normalized * speed * Time.deltaTime);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(cameraRay, out RaycastHit hit, 500, whatIsGround))
        {
            Vector3 directionToLook = (hit.point - transform.position).normalized;
            directionToLook.y = transform.position.y; //No cambio la altura.
            Quaternion rotationToLook = Quaternion.LookRotation(directionToLook);
            transform.rotation = rotationToLook;
                //Debug.DrawRay(cameraRay.origin, cameraRay.direction * 500, Color.red, 10);

            
        }
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        
    }
    // Update is called once per frame          
    void FixedUpdate()
    {

        //if (groundPlane.Raycast(cameraRay, out)
        //{

        //}
        
    }

    //private void FixedUpdate()
    //{
    //    //ForceMode.Force: fuerza continua (FIXED UPDATE)
    //    //ForceMode.Impulse: fuerza INSTANT�NEA.
    //    rb.AddForce(new Vector3(inputH, 0, inputV) * speed ,ForceMode.Impulse);
    //}
}
