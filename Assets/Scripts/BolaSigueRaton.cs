using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaSigueRaton : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] GameObject bola;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 500, whatIsGround)) 
        {
            Instantiate(bola, hitInfo.transform.position, Quaternion.identity);
        }
    }
}
