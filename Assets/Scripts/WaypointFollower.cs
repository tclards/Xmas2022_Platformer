using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int curWaypoint = 0;

    [SerializeField] private float fSpeed = 2f;

    private void Update()
    {
        // if curWaypoint and platform are close/in contact, switch to next waypoint
        if (Vector2.Distance(waypoints[curWaypoint].transform.position, transform.position) < 0.1f)
        {
            curWaypoint++;
            if (curWaypoint >= waypoints.Length) // if at last waypoint, reset back to first waypoint
            {
                curWaypoint = 0;
            }
        }

        // move platform towards current waypoint
        transform.position = Vector2.MoveTowards(transform.position, waypoints[curWaypoint].transform.position, Time.deltaTime * fSpeed);
    }
}
