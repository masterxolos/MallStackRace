using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointController : MonoBehaviour
{
    [SerializeField]
    private Transform mytransform;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private List<Transform> wayPoint = new List<Transform>();
    private Transform targetWaypoint;

    private int targetWaypointIndex = 0;
    private int lastWaypointIndex = 7;

    private float minDistance = 0.1f;

    [SerializeField] private float movementSpeed = 3;
    


    void Start()
    {
        targetWaypoint = wayPoint[0];
    }

    
    void Update()
    {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = rotationSpeed * Time.deltaTime;

        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        Debug.DrawRay(transform.position, transform.forward * 50f, Color.green, 0f); //Draws a ray forward in the direction the enemy is facing
        Debug.DrawRay(transform.position, directionToTarget, Color.red, 0f); //Draws a ray in the direction of the current target waypoint

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        CheckDistanceToWaypoint(distance);

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);

    }

    private void CheckDistanceToWaypoint(float currentDistance)
    {
        if (currentDistance <= minDistance)
        {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    private void UpdateTargetWaypoint()
    {
        if (targetWaypointIndex >= lastWaypointIndex)
        {
            targetWaypointIndex = 0;
        }
        
        targetWaypoint = wayPoint[targetWaypointIndex];
    }
}
