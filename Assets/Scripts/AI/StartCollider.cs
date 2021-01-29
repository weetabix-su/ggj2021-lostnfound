using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollider : MonoBehaviour
{
    public delegate void EnterStart();
    public static event EnterStart OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnEnter != null)
            {
                OnEnter();
            }
        }
    }
}
