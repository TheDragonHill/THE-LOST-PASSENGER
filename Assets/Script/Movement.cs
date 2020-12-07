using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject waypoints;
    public List<GameObject> waypoint = new List<GameObject>();

    public int current = 0;
    public float speed = 1;

    public DecisionManager decisionManager;

    public bool isStop = false;

    public Gamemanager gamemanager;

    private void Start()
    {
        //Search all waypoints and put it in the waypoint List
        foreach (Transform children in waypoints.transform)
        {
            waypoint.Add(children.gameObject);
        }

        //Give the savedDecisions List the same length as the waypoint List
        decisionManager.savedDecisions = new int[waypoint.Count];
    }

    void Update()
    {
        //Checks whether the player is allowed to move
        if (!isStop && current != waypoint.Count)
        {
            //Checks whether the player passes a waypoint
            if (Vector3.Distance(waypoint[current].transform.position, transform.position) == 0)
            {
                //Start function if Waypoint is hited
                HitWaypoint();
            }

            //Moves the player
            transform.position = Vector3.MoveTowards(transform.position, waypoint[current].transform.position, Time.deltaTime * speed);
        }
    }

    public void HitWaypoint()
    {
        //When the player passes a waypoint, the system saves
        gamemanager.GetComponent<Gamemanager>().Save();

        //Checks whether the waypoint contains an event that can then be started
        if (waypoint[current].GetComponent<EventTrigger>())
        {
            isStop = true;
            waypoint[current].GetComponent<EventTrigger>().StartEvent();
        }

        current++;
    }
}
