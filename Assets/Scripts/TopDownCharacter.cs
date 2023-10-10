using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacter : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField] private float moveSpeed;
 

    // Start is called before the first frame update
    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //Moverse 
        MoveTowardTarget (targetVector);
    }

    private void MoveTowardTarget(Vector3 targetVector)
    {
       var speed = moveSpeed * Time.deltaTime;
       transform.Translate(targetVector * speed);
    }
}
