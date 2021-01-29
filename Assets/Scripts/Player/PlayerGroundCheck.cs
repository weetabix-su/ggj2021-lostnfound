﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] [Range(0.1f, 5f)] float detectDistance = 0.1f;
    public bool isGrounded;
    Collider col;

    void OnEnable()
    {
        isGrounded = false;
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = ((groundLayers.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}