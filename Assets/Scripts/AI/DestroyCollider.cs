using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    public delegate void EnterTrigger();
    public static event EnterTrigger OnEnter;

    public bool lastCalled = false;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (lastCalled == false)
        {
            if (OnEnter != null)
            {
                OnEnter();
                lastCalled = true;
            }
        }
        else
        {
            lastCalled = false;
            return;
        }
    }
}
