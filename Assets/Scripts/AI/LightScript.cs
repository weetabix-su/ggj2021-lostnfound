using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public Vector3 originalPosition;
    public Transform reference;
    public float diff;

    private void Start()
    {
        originalPosition = transform.position;
        diff = reference.position.x - originalPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (reference == null)
        {
            Destroy(gameObject);
        }
        if (Vector3.Distance(transform.position, new Vector3(-6.300614f, 6.97f, 14.45f)) <= 0.1f)
            Destroy(gameObject);
        transform.position = new Vector3(reference.position.x - diff, originalPosition.y, originalPosition.z);
    }
}
