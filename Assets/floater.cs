﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour
{
    public Rigidbody rb;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    private void FixedUpdate()
    {
        if (transform.position.y < 0f)
        {
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y/depthBeforeSubmerged) * displacementAmount;
                
        }
    }


}
