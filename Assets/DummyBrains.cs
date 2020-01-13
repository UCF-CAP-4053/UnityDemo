using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBrains : MonoBehaviour
{
    public List<Transform> Waypoints;

    private Animator AnimationController;
    private Vector3 Velocity;
    private int WaypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        AnimationController = GetComponent<Animator>();
        WaypointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var distanceToTarget = Vector3.Distance(transform.position, Waypoints[WaypointIndex].transform.position);
        if (distanceToTarget < 0.5f) {
            WaypointIndex++;
            WaypointIndex %= Waypoints.Count;
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            Waypoints[WaypointIndex].transform.position,
            ref Velocity,
            0.6f
        );

        AnimationController.SetFloat("Speed", Velocity.magnitude);
    }
}
