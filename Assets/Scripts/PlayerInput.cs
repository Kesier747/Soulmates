using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    Rigidbody rb;

    private float inputH;
    private float inputV;
 
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
        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");
       
        controller.Move(new Vector3(inputH, 0, inputV).normalized * speed * Time.deltaTime);

        //Para el dash y la corrutina del dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !dashing)
        { 
            StartCoroutine(Dash());
        }

        //Todo esto para que el Player mire en dirección de donde la cámara este apuntando
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(cameraRay, out RaycastHit hit, 500, whatIsGround) && Time.timeScale!= 0)
        {
            Vector3 directionToLook = (hit.point - transform.position).normalized;
            directionToLook.y = transform.position.y; //No cambio la altura.
            Quaternion rotationToLook = Quaternion.LookRotation(directionToLook);
            transform.rotation = rotationToLook; //La rotación del Player
                //Debug.DrawRay(cameraRay.origin, cameraRay.direction * 500, Color.red, 10);
        }
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
