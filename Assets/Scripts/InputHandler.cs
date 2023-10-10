using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class InputHandler : MonoBehaviour
{
    [SerializeField] public Vector2 InputVector { get; set; }

    [SerializeField] public Vector3 MousePosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var h = UnityEngine.Input.GetAxis("Horizontal");
        var v = UnityEngine.Input.GetAxis("Verical");
        InputVector = new Vector2(h, v);

        MousePosition = UnityEngine.Input.mousePosition;
    }
}
