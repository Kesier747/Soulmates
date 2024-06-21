using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private bool rifleHeld = false;

    private float overheat;
    private bool overheated = false;
    [SerializeField] private GameObject pistolBulletPrefab;
    [SerializeField] private GameObject rifleBulletPrefab;
    [SerializeField] private GameObject pistolBulletSpawnPoint;
    [SerializeField] private GameObject submachinegunBulletSpawnPoint;
    [SerializeField] private GameObject rifleBulletSpawnPoint;
    [SerializeField] private float pistolDamage;
    [SerializeField] private float SubmachinegunDamage;

    private int currentLife;
    [SerializeField] private int lifeInit;
    public bool deadState = false;
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private GameObject[] lifes;

    private bool yellowKeyAdquired = false;

    private bool WeaponUIActive = true;
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject endMenuUI;
    [SerializeField] private GameObject deathMenuUI;
    [SerializeField] private GameObject pistolIconUI;
    [SerializeField] private GameObject SubmachinegunIconUI;
    [SerializeField] private GameObject rifleIconUI;

    //[SerializeField] private GameObject heatBarUI;
    [SerializeField] public Image heatCooldown;

    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject submachinegun;
    [SerializeField] private GameObject rifle;

    private string activeWeapon = "hola";

    private Camera mainCamera;
    [SerializeField] private LayerMask whatIsGround;
    private float RayLength;

    CharacterController controller;
   
    void Start()
    {
        activeWeapon = "Pistol";
        currentLife = lifeInit;
        controller = GetComponent<CharacterController>();
        spawnPoint = transform.position;
        mainCamera = FindObjectOfType<Camera>();

        gameplayUI.SetActive(true);

        pistolHeld = true;

        

        //Debug.log(pistolHeld + rifleHeld + submachinegunHeld);
        pistolIconUI.SetActive(false);
        overheat = 0;
        overheated= false;

        EquippingPistol();
    }

    private void Update()
    {
        //UnityEngine.Debug.log("arma activa = " + activeWeapon);
        Debug.Log(activeWeapon);
        if(pistolHeld == true)
        {
            pistolHeld = true;
        }

        playerHealthText.text = "Vida = " + currentLife;

        if (Time.timeScale != 0)
        {
            Shoot();
            WeaponUIActive = true;
            //gameplayUI.setActive(true);
        }

        if(Time.timeScale == 0) 
        {
            UnequippingAnyWeapon();
            WeaponUIActive = false;
            //gameplayUI.setActive(false);
        }

        ApplyMovement();
        ApplyRotation();
        ApplyGravity();

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        { 
            StartCoroutine(Dash());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && WeaponUIActive == true || activeWeapon == "Rifle" || activeWeapon == "SMG"/*&& pistolHeld == true*/)
        {
            EquippingPistol();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && WeaponUIActive == true /*&& submachinegunHeld == true*/)
        {
            EquippingSubmachinegun();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && WeaponUIActive == true /*&& rifleHeld == true*/)
        {
            EquippingRifle();
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
        rifleHeld = false;
        rifle.SetActive(false);
        rifleIconUI.SetActive(false);
    }

    private void EquippingSubmachinegun()
    {
        submachinegunHeld = true;
        submachinegun.SetActive(true);
        SubmachinegunIconUI.SetActive(true);

        pistolHeld = false;
        pistol.SetActive(false);
        pistolIconUI.SetActive(false);
        rifleHeld = false;
        rifle.SetActive(false);
        rifleIconUI.SetActive(false);

    }

    private void EquippingRifle()
    {
        rifleHeld = true;
        rifle.SetActive(true);
        rifleIconUI.SetActive(true);

        pistolHeld = false;
        pistol.SetActive(false);
        pistolIconUI.SetActive(false);
        submachinegunHeld = false;
        submachinegun.SetActive(false);
        SubmachinegunIconUI.SetActive(false);
    }

    private void UnequippingAnyWeapon()
    {
        pistolIconUI.SetActive(false);
        SubmachinegunIconUI.SetActive(false);
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
                GameObject bulletClone = Instantiate(pistolBulletPrefab, pistolBulletSpawnPoint.transform.position, transform.rotation);
                overheat += 10f;
            }
        }

        else if (submachinegunHeld == true && overheat <= 100 && overheated == false)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                GameObject bulletClone = Instantiate(pistolBulletPrefab, submachinegunBulletSpawnPoint.transform.position, transform.rotation);
                overheat += 2f;
            }
        }

        else if (rifleHeld == true && overheat <= 100 && overheated == false)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject bulletClone = Instantiate(rifleBulletPrefab, rifleBulletSpawnPoint.transform.position, transform.rotation);
                overheat += 40f;
            }
        }
    }

    private void HeatCooling()
    {
        if (overheated == false)
        {
            overheat -= 30 * Time.deltaTime;
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

        heatCooldown.fillAmount = overheat/100f;

        //heatBarUI.fillAmount = overheat;
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
            gameplayUI.SetActive(false);
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

        if (other.gameObject.CompareTag("EnemyDrone"))
        {
            currentLife -= 20;
            for (int i = currentLife / 10; i < lifeInit / 10; i++)
            {
                lifes[i].SetActive(false);
            }

            if (currentLife <= 0)
            {
                Death();
            }
        }

        if (other.gameObject.CompareTag("SMGPickup"))
        {
            submachinegunHeld = true;
            Destroy(other.gameObject);
            activeWeapon = "SMG";
        }

        if (other.gameObject.CompareTag("RiflePickup"))
        {
            rifleHeld = true;
            Destroy(other.gameObject);
            activeWeapon = "Rifle";
        }
    }
}
