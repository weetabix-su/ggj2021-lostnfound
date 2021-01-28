using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrabInteractEvent : GrabInteract
{
    [Header("Parameters")]
    [SerializeField] UnityEvent GrabEvent;
    [SerializeField] UnityEvent ReleaseEvent;

    public override void OnGrab()
    {
        GrabEvent.Invoke();
    }

    public override void OnRelease()
    {
        ReleaseEvent.Invoke();
    }
}
