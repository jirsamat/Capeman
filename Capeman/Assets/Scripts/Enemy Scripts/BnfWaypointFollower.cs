using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Back and forth waypoint follower
public class BnfWaypointFollower : MonoBehaviour
{
    //creates a list of waypoints for scalability
    [SerializeField] private GameObject[] waypoints;
    
    //index for current targeted waypoint
    private int currentWaypointIndex = 0;
    //used for cycling waypoints
    private int cycleValue = -1;

    [SerializeField] private float speed = 5f;

    private void Update()
    {
        //checks the distance of the currently targeted waypoint - if its reached - cycles the index
        if(Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            //checks for completion of the cycle - reverses it
            if (currentWaypointIndex >= waypoints.Length-1 || currentWaypointIndex <= 0)
            {
                cycleValue = -1 * cycleValue;
            }
            currentWaypointIndex += cycleValue;
        }
        //moves the platform
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);

    }
}
