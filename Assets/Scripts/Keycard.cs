using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.eulerAngles += new Vector3(0, 1, 0) * rotationSpeed * Time.deltaTime;

    }

    private void OnDestroy()
    {

    }
}
