using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpTest : MonoBehaviour
{
    public Rigidbody rb;
    public float timer = 0f;
    public bool jumping;
    public bool isGround;
    public float timeToWait;

    public bool activated = true;

    private void OnDestroy()
    {
        LevelController.OnStart -= SetInactived;
        LevelController.OnEnd -= SetActived;
    }

    private void OnEnable()
    {
        LevelController.OnStart += SetInactived;
        LevelController.OnEnd += SetActived;
        timer = 0f;
        isGround = true;
    }

    void SetActived()
    {
        activated = true;
    }

    void SetInactived()
    {
        activated = false;
    }


    private void Update()
    {
        if (!activated)
            return;
        if (jumping)
        {
            timer += Time.deltaTime;
        }

        if (rb.velocity.y <= 0.1f && timer >= timeToWait)
        {
            isGround = true;
            jumping = false;
            timer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !jumping && isGround)
        {
            jumping = true;
            isGround = false;
            rb.AddForce(Vector2.up * 6.5f, ForceMode.Impulse);
        }
    }
}
