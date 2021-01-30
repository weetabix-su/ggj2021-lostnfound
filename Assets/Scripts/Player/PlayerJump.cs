using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] [Range(1f, 10f)] float jumpVelocity = 2.5f;
    [SerializeField] [Range(1f, 10f)] float fallMultiplier = 2.5f;
    [SerializeField] [Range(1f, 10f)] float lowJumpMultiplier = 2f;
    [SerializeField] bool useAnimatorJumpTrigger = true;

    Rigidbody rb;
    Collider col;
    PlayerGroundCheck gc;
    PlayerAnimControl ac;
    PlayerSFX sfx;
    bool isJump;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        col = GetComponent<Collider>();
        sfx = GetComponent<PlayerSFX>();
        gc = GetComponent<PlayerGroundCheck>();
        ac = GetComponent<PlayerAnimControl>();
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            rb.AddForce(Vector2.up * jumpVelocity, ForceMode.Impulse);
            if (ac != null && useAnimatorJumpTrigger) ac.Jump();
            if (sfx != null) sfx.Jump();
            isJump = false;
        }

        rb.AddForce(Physics.gravity * ((rb.velocity.y < 0) ? fallMultiplier : ((rb.velocity.y > 0 && !Input.GetButton("Jump")) ? lowJumpMultiplier : 1f)), ForceMode.Acceleration);

        if (rb.velocity.y < 0) rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }

    private void Update()
    {
        if (gc == null && Input.GetButtonDown("Jump")) isJump = true;
        else if (gc != null && gc.isGrounded && Input.GetButtonDown("Jump")) isJump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gc != null && gc.groundVerify(collision.gameObject) && sfx != null) sfx.Land();
    }
}
