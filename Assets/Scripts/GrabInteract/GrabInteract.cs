using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GrabInteract : MonoBehaviour
{
    Collider col;

    public virtual void OnEnable()
    {
        col = GetComponent<Collider>(); 
    }

    public abstract void OnGrab();

    public abstract void OnRelease();
}
