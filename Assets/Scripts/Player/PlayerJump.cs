using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] [Range(1f, 10f)] float jumpVelocity = 2.5f;
    [SerializeField] [Range(1f, 10f)] float fallMultiplier = 2.5f;
    [SerializeField] [Range(1f, 10f)] float lowJumpMultiplier = 2f;

    Rigidbody rb;
    float gravityScale = 1f;
    bool isJump;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode.Impulse);
            isJump = false;
        }

        rb.AddForce(Physics.gravity * ((rb.velocity.y < 0) ? fallMultiplier : ((rb.velocity.y > 0 && !Input.GetButton("Jump")) ? lowJumpMultiplier : 1f)), ForceMode.Acceleration);

        //if (rb.velocity.y < 0) rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        //else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) isJump = true;
    }
}
