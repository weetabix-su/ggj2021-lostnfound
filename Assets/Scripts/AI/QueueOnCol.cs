using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueOnCol : MonoBehaviour
{
    public delegate void OnCollide();
    public static event OnCollide OnCol;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnCol != null)
                OnCol();
        }
    }
}
