using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField][Range(0.1f, 20f)] float playerSpeed = 3f;
    [SerializeField] bool flipX;
    [SerializeField] bool flipY;
    [SerializeField] bool checkGround = false;

    PlayerGroundCheck gc;

    private void OnEnable()
    {
        gc = GetComponent<PlayerGroundCheck>();
    }

    void FixedUpdate()
    {
        // If PlayerGroundCheck is on the same gameObject and checkGround is enabled, check if player is grounded
        if (gc != null && checkGround && !gc.isGrounded) return;
        // Moves player depending on Input Axis
        transform.position += new Vector3(Input.GetAxis("Horizontal") * (flipX ? -1 : 1), 0, Input.GetAxis("Vertical") * (flipY ? -1 : 1)) * playerSpeed * Time.deltaTime;
    }
}
