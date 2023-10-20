using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public GameObject crosshair;
    public GameObject player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));

        Vector3 diff = target - player.transform.position;
        float rotationY = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        player.transform.rotation = Quaternion.Euler(0.0f, rotationY, 0.0f);
    }
}
