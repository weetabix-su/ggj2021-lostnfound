using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    Collider col;
    GrabInteract grab;
    GameObject grabObject;

    private void OnEnable()
    {
        col = GetComponent<Collider>();
    }

    void Update()
    {
        if (grab == null) return;

        // I assume Fire1 is assigned to E or the Xbox X button
        if (Input.GetButtonDown("Fire1")) grab.OnGrab();
        else if (Input.GetButtonUp("Fire1")) grab.OnRelease();
    }

    private void OnCollisionEnter(Collision collision)
    {
        grab = collision.gameObject.GetComponent<GrabInteract>();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (grab == collision.gameObject.GetComponent<GrabInteract>()) grab = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        grab = other.gameObject.GetComponent<GrabInteract>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (grab == other.gameObject.GetComponent<GrabInteract>()) grab = null;
    }
}
