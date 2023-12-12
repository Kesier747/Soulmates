using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{

    Rigidbody rb;

    private float inputH;
    private float inputV;

    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier;
    private float velocity;
    private Vector3 verticalMovement;
 
    [SerializeField] private float speed;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float stamina;

    private bool dashing = false;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private Vector3 spawnPoint;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletSpawnPoint;

    [SerializeField] private float lifeTotal;

    private Camera mainCamera;
    [SerializeField] private LayerMask whatIsGround;
    private float RayLength;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        spawnPoint = transform.position;
        mainCamera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Shoot();
        }

        ApplyMovement();
        ApplyRotation();
        ApplyGravity();

        //Para el dash y la corrutina del dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        { 
            StartCoroutine(Dash());
        }

    }

    private void ApplyMovement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        controller.Move(new Vector3(inputH, 0, inputV).normalized * speed * Time.deltaTime);
    }

    private void ApplyRotation()
    {
        //Todo esto para que el Player mire en dirección de donde la cámara este apuntando
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out RaycastHit hit, 1000, whatIsGround) && Time.timeScale != 0)
        {
            Vector3 directionToLook = (hit.point - transform.position).normalized;
            directionToLook.y = 0; //No cambio la altura.
            Quaternion rotationToLook = Quaternion.LookRotation(directionToLook);
            transform.rotation = rotationToLook; //La rotación del Player
                                                 //Debug.DrawRay(cameraRay.origin, cameraRay.direction * 500, Color.red, 10);
        }
    }

    private void ApplyGravity()
    {
        velocity += gravity * gravityMultiplier * Time.deltaTime;
        controller.Move (new Vector3(0,velocity,0) * Time.deltaTime);
    }

    private void Shoot() //Pues pa disparar
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, transform.rotation);

            //spawnPoint.transform.position, transform.rotation
        }
    }

    private IEnumerator Dash() //El dash y eso
    {
        dashing = true;
        controller.Move(new Vector3(inputH, 0, inputV).normalized * dashPower * Time.deltaTime);
        yield return dashing = false;

    }

}
