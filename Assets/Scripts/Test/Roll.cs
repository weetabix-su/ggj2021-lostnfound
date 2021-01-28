using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    public Rigidbody rb;
    public Collider collider;
    public int maxAngle;

    public int maxAngularVel = 7;

    public Vector3 originalPosition;
    public Vector3 closestPoint;

    public Transform GO;


    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        collider = transform.GetComponent<Collider>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = originalPosition;
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(transform.up);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(transform.up * -1);
        }
    }

    private void FixedUpdate()
    {
        closestPoint = collider.ClosestPoint(GO.position);
        Debug.Log(Mathf.Abs(originalPosition.y - closestPoint.y));
        if(Mathf.Abs(originalPosition.y - closestPoint.y) > 0.55f || Mathf.Abs(originalPosition.y - closestPoint.y) < 0.45f)
            rb.AddRelativeTorque((closestPoint.z < 0? Vector3.up : Vector3.down) * Mathf.Abs(originalPosition.y - closestPoint.y) * 1);
        rb.angularDrag = Mathf.Pow(Mathf.Abs(rb.angularVelocity.x) * 5, 2);
    }
}
