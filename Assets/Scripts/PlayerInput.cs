using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    public bool deadState = false;

    private bool yellowKeyAdquired = false;
    [SerializeField] private GameObject endMenuUI;
    [SerializeField] private GameObject deathMenuUI;


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
        Debug.Log(lifeTotal + " llave : " + yellowKeyAdquired);

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

        if(lifeTotal <= 0)
        {
            Death();
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

        if (TouchingGround()) 
        {
            velocity = -7;
        }
    }

    bool TouchingGround()
    {
        bool TouchingGround = Physics.Raycast(transform.position, new Vector3(0, -1, 0), 3f, whatIsGround);
        return TouchingGround;
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

    private void Death()
    {
            Time.timeScale = 0f;
            deathMenuUI.SetActive(true);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathFloor"))
        {
            lifeTotal -= 1000;
        }

        if (other.gameObject.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            yellowKeyAdquired = true;
        }

        if (other.gameObject.CompareTag("YellowDoor") && yellowKeyAdquired == true)
        {
            Time.timeScale = 0f;
            endMenuUI.SetActive(true);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            lifeTotal -= 20;
        }
    }

    //private void DeathState()
    //{
    //    if(lifeTotal <= 0) 
    //    {
    //        deadState = false;
    //    }
    //    else
    //    {
    //        deadState = true;
    //    }
    //}
}
