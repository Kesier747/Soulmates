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
        //Falta sacar la información de HitInfo del raycast, porque no me está sacando la info.
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 500, whatIsGround)) 
        {
            Debug.Log(hitInfo.transform.position);
            Instantiate(bola, hitInfo.transform.position, Quaternion.identity);
        }

        transform.eulerAngles += new Vector3(0, 1, 0) * Time.deltaTime;
        //Space.World: Coordenadas globales.
        //Space.Self: (Por defecto) Coordenadas locales.
        transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime, Space.World);
    }
}
