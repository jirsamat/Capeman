using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

//isnt used
public class EnemyAI : MonoBehaviour
{
    public GameObject target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        //updates the enemys path every half second
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        }
    }

    //checks if the current path is completed
    void OnPathComplete(Path p)
    {
        //checks if we didn't get any errors and then sets up the path
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //checks if we have a path
        if (path == null)
        {
            return;
        }
        //checks if there are more waypoints in the path - didn't reach the end
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        //sets direction from current position to the current waypoint, .normalized makes its lenght 1
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;     
        //framerate independent force to move the enemy
        Vector2 force = direction * speed * Time.deltaTime;
        rb.AddForce(force);
        //finds the distance to current waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        //moves on to the next waypoint if the current one has been reached
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //flips the enemy
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
