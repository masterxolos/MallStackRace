using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    //public float speed;
    public DynamicJoystick variableJoystick;
    public Rigidbody rb;

    //public CharacterController characterController;

    [SerializeField] public float rotationSpeed = 20;
    public void FixedUpdate()
    {
        //Vector3 direction = Vector3.forward * variableJoystick.Vertical * 2 + Vector3.right * variableJoystick.Horizontal;
        
        //transform.Translate(direction * speed * Time.deltaTime);
        transform.position += (Vector3.forward * variableJoystick.Vertical)/2 + (Vector3.right * variableJoystick.Horizontal)/2;
        //characterController.Move(direction * speed * Time.fixedDeltaTime);
        
        //float angleA = Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, angleA, 0f);
        
        Quaternion currentQ = Quaternion.LookRotation(new Vector3(variableJoystick.Horizontal * 1000, 0, variableJoystick.Vertical * 1000) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, currentQ, rotationSpeed * Time.deltaTime);
        


    }
}