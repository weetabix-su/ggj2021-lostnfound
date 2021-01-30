using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class QueueBehind : MonoBehaviour
{
    [Header("Settings")]
    public float waitingTime;
    public int numberOfHumans;
    public float walkingSpeed;

    [HideInInspector]
    public List<Transform> humans;
    [HideInInspector]
    public List<NavMeshAgent> agents;
    [HideInInspector]
    public List<Vector3> locations;

    public bool activate = false;

    private void Awake()
    {
        activate = false;
    }

    void Start()
    {
        for (int i = 0; i < numberOfHumans; i++)
        {
            humans.Add(transform.GetChild(i));
            agents.Add(humans[i].GetComponent<NavMeshAgent>());
            agents[i].speed = walkingSpeed;
        }
        for (int i = numberOfHumans; i < numberOfHumans * 2; i++)
        {
            locations.Add(transform.GetChild(i).transform.position);
        }
    }

    private void Update()
    {
        if (activate)
        {
            StartSet();
        }
    }


    void StartQueue()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].SetDestination(locations[i]);
        }
        activate = true;
    }

    void StartSet()
    {
        List<Vector3> temp = Queue.instance.GetLocations();
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents[i] == null)
            {
                continue;
            }
            // Check if we've reached the destination
            if (!agents[i].pathPending)
            {
                if (agents[i].remainingDistance <= agents[i].stoppingDistance)
                {
                    if (!agents[i].hasPath || agents[i].velocity.sqrMagnitude == 0f)
                    {
                        agents[i].SetDestination(temp[i]);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartQueue();
        }
    }
}
