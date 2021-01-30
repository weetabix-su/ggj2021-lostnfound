using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewQueue : MonoBehaviour
{
    public static NewQueue instance;

    [Header("Settings")]
    public float waitingTime;
    public int numberOfHumans;
    public float walkingSpeed;

    [HideInInspector]
    public List<Transform> humans;
    //[HideInInspector]
    public List<NavMeshAgent> agents;
    [HideInInspector]
    public List<Vector3> locations;
    [Space(5)]
    public GameObject destroyCollider;
    [Space(5)]
    public Light light;

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
        for (int i = numberOfHumans; i < numberOfHumans * 2 + 1; i++)
        {
            locations.Add(transform.GetChild(i).transform.position);
        }
        Destroy(transform.GetChild(numberOfHumans).gameObject);

        destroyCollider.SetActive(true);
        DestroyCollider.OnEnter += RemoveHuman;
        //StartCollider.OnEnter += StartQueue;
        //tartWait();
    }

    private void OnDestroy()
    {
        DestroyCollider.OnEnter -= RemoveHuman;
        //StartCollider.OnEnter -= StartQueue;
    }

    void StartQueue()
    {
        if(light != null)
            light.enabled = true;
        if (agents.Count <= 0)
        {
            if (OnEnd != null)
                OnEnd();
            return;
        }
        for (int i = 0; i < agents.Count; i++)
        {
            if (agents.Count > 3)
            {
                if (i < agents.Count - 3)
                    agents[i].SetDestination(locations[i]);
                else
                    agents[i].SetDestination(locations[i + 1]);
            }
            else
                agents[i].SetDestination(locations[i]);
        }
        StartWait();
    }

    void StartWait()
    {
        if(light != null)
            light.enabled = true;
        Invoke("StartQueue", waitingTime);
    }

    void RemoveHuman()
    {
        Debug.Log("Called");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartWait();
        }
    }
}
