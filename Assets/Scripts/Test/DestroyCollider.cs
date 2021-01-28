using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    public delegate void EnterTrigger();
    public static event EnterTrigger OnEnter;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (OnEnter != null)
        {
            OnEnter();
        }
    }
}
