using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
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
    [Space(5)]
    public GameObject destroyCollider;

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
        destroyCollider.SetActive(true);
        DestroyCollider.OnEnter += RemoveHuman;
        StartQueue();
    }

    private void OnDestroy()
    {
        DestroyCollider.OnEnter -= RemoveHuman;
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
