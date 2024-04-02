using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cyclic waypoint follower - not used
public class CklWaypointFollower : MonoBehaviour
{
    //creates a list of waypoints for scalability
    [SerializeField] private GameObject[] waypoints;

    //index for current targeted waypoint
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 5f;

    private void Update()
    {
        //checks the distance of the currently targeted waypoint - if its reached - cycles the index
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            //checks for completion of the cycle - resets it
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        //moves the platform
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    }
}