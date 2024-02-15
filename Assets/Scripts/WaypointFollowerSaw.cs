using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerSaw : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 5f;

    private void Start()
    {
        MoveToNextWaypoint();
    }

    private void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypointIndex].transform.position;

        LeanTween.move(gameObject, targetPosition, Vector3.Distance(transform.position, targetPosition) / speed)
                 .setEase(LeanTweenType.linear)
                 .setOnComplete(OnWaypointReached);
    }

    private void OnWaypointReached()
    {
        currentWaypointIndex++;
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
        }

        MoveToNextWaypoint();
    }
}
