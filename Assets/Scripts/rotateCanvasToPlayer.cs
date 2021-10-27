using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCanvasToPlayer : MonoBehaviour
{
    [SerializeField] private int movementSpeed = 5;

    [SerializeField] private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTransform.position);
    }
}
