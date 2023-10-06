using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtra : MonoBehaviour
{
    Camera cam;
    Vector3 distanceToPlayer;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject bola;
    private void Awake()
    {
        cam = Camera.main;
        distanceToPlayer = transform.position - cam.transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 500, whatIsGround))
        {
            if(Input.GetMouseButtonDown(0))
            {
                Instantiate(bola, hitInfo.transform.position, Quaternion.identity);

            }
        }
    }
}
