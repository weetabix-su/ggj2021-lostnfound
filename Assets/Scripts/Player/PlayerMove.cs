using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField][Range(0.1f, 20f)] float playerSpeed = 3f;
    //[SerializeField] bool checkGround = true;
    //[SerializeField] LayerMask filterGround;

    // Insert Raycast elements here

    void FixedUpdate()
    {
        // Moves player depending on Input Axis
        // Ideally there should be a Raycast checking if player is on the ground
        transform.position += new Vector3(Input.GetAxis("Horizontal"), transform.position.y, Input.GetAxis("Vertical")) * playerSpeed * Time.deltaTime;
    }
}
