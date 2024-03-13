using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private bool pistolHeld = false;
    private bool submachinegunHeld = false;

    private float overheat;
    private bool overheated = false;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject pistolBulletSpawnPoint;
    [SerializeField] private GameObject submachinegunBulletSpawnPoint;
    [SerializeField] private float pistolDamage;
    [SerializeField] private float SubmachinegunDamage;

    private int currentLife;
    [SerializeField] private int lifeInit;
    public bool deadState = false;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private GameObject[] lifes;

    private bool yellowKeyAdquired = false;
    [SerializeField] private GameObject endMenuUI;
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private GameObject pistolIconUI;
    [SerializeField] private GameObject SubmachinegunIconUI;
    [SerializeField] private GameObject swordIconUI;

    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject submachinegun;

    private Camera mainCamera;
    [SerializeField] private LayerMask whatIsGround;
    private float RayLength;

    CharacterController controller;
   
    void Start()
    {
        currentLife = lifeInit;
        controller = GetComponent<CharacterController>();
        spawnPoint = transform.position;
        mainCamera = FindObjectOfType<Camera>();

        //submachinegunHeld= true;
        pistolHeld = false;
        pistolIconUI.SetActive(false);
        overheat = 0;
        overheated= false;

        EquippingPistol();
    }

    private void Update()
    {
        playerHealthText.text = "Vida = " + currentLife;

        if (Time.timeScale != 0)
        {
            Shoot();
        }

        ApplyMovement();
        ApplyRotation();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        { 
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EquippingPistol();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            EquippingSubmachinegun();
        }

        HeatCooling();
        Debug.Log(overheat);
        Debug.Log("heated = " + overheated);
    }

    private void EquippingPistol()
    {
        pistolHeld = true;
        pistol.SetActive(true);
        pistolIconUI.SetActive(true);

        submachinegunHeld = false;
        submachinegun.SetActive(false);
        SubmachinegunIconUI.SetActive(false);
    }

    private void EquippingSubmachinegun()
    {
        submachinegunHeld = true;
        submachinegun.SetActive(true);
        SubmachinegunIconUI.SetActive(true);

        pistolHeld = false;
        pistol.SetActive(false);
        pistolIconUI.SetActive(false);
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
            transform.rotation = rotationToLook; //La rotación del Player //Debug.DrawRay(cameraRay.origin, cameraRay.direction * 500, Color.red, 10);
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
        if (pistolHeld == true && overheat <= 100 && overheated == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bulletClone = Instantiate(bulletPrefab, pistolBulletSpawnPoint.transform.position, transform.rotation);
                overheat += 10;
            }
        }

        else if (submachinegunHeld == true && overheat <= 100 && overheated == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject bulletClone = Instantiate(bulletPrefab, submachinegunBulletSpawnPoint.transform.position, transform.rotation);
                overheat += 1f;
            }
        }
    }

    private void HeatCooling()
    {
        if (overheated == false)
        {
            overheat -= 5 * Time.deltaTime;
        }

        if (overheat <= 0)
        {
            overheat = 0;
        }

        if (overheat >= 100 && overheated == false)
        {
            overheated = true;
        }

        if (overheated == true)
        {
            overheat -= 25 * Time.deltaTime;

            if(overheat <= 0 && overheated == true)
            {
                overheated = false;
            }
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
            currentLife -= 1000;
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
            currentLife -= 20;
            for (int i = currentLife /10; i < lifeInit /10; i++)
            {
                lifes[i].SetActive(false);
            }
           
            if(currentLife <= 0)
            {
                Death();
            }
        }
    }
}
