using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Queue : MonoBehaviour
{
    public static Queue instance;

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

    public delegate void EndQueue();
    public static event EndQueue OnEnd;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.Log("FAILURE");
            Destroy(gameObject);
        }
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
        for (int i = 0; i < 3; i++)
        {
            locations.Add(transform.GetChild(numberOfHumans * 2 + i).transform.position);
        }
        destroyCollider.SetActive(true);
        DestroyCollider.OnEnter += RemoveHuman;
        StartCollider.OnEnter += StartQueue;
    }

    private void OnDestroy()
    {
        DestroyCollider.OnEnter -= RemoveHuman;
        StartCollider.OnEnter -= StartQueue;
    }

    void StartQueue()
    {
        if (agents.Count <= 0)
        {
            if (OnEnd != null)
                OnEnd();
            return;
        }
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
        if (humans.Count > 0)
            humans.RemoveAt(0);
        if (agents.Count > 0)
            agents.RemoveAt(0);
    }

    public List<Vector3> GetLocations()
    {
        List<Vector3> temp = new List<Vector3>();
        for (int i = agents.Count; i < agents.Count + 3; i++)
        {
            temp.Add(locations[i]);
        }
        return temp;
    }
}
