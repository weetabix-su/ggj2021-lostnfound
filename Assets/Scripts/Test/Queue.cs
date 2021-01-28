using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    public float waitingTime;
    public int numberOfHumans;

    public List<Transform> humans;
    public List<NavMeshAgent> agents;
    public List<Vector3> locations;
    public GameObject destroyCollider;



    void Start()
    {
        for (int i = 0; i < numberOfHumans; i++)
        {
            humans.Add(transform.GetChild(i));
            agents.Add(humans[i].GetComponent<NavMeshAgent>());
        }
        for (int i = numberOfHumans; i < numberOfHumans * 2; i++)
        {
            locations.Add(transform.GetChild(i).transform.position);
        }
        destroyCollider.SetActive(true);
        DestroyCollider.OnEnter += RemoveHuman;
        StartQueue();
    }

    void StartQueue()
    {
        if (agents.Count <= 0)
            return;
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].SetDestination(locations[i]);
        }
        StartWait();
    }

    void StartWait()
    {
        Invoke("StartQueue", waitingTime);
    }

    void RemoveHuman()
    {
        humans.RemoveAt(0);
        agents.RemoveAt(0);
    }
}
