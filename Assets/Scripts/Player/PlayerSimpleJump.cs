using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleJump : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] [Range(1f, 10f)] float jumpVelocity = 2.5f;

    Rigidbody rb;
    PlayerGroundCheck gc;
    bool isJump;

    void Start()
    {
        // Disables PlayerJump when detected
        PlayerJump pj = GetComponent<PlayerJump>();
        if (pj != null) pj.enabled = false;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        gc = GetComponent<PlayerGroundCheck>();
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode.Impulse);
            isJump = false;
        }
    }

    private void Update()
    {
        if (gc == null && Input.GetButtonDown("Jump")) isJump = true;
        if (gc != null && gc.isGrounded && Input.GetButtonDown("Jump")) isJump = true;
    }
}
