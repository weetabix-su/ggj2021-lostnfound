using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTest : MonoBehaviour
{
    public Rigidbody rb;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.forward * -1 * Time.deltaTime);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.left * -1 * Time.deltaTime);
    }
}
