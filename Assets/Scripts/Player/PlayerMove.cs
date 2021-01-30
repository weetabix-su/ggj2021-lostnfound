using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum moveMode { xOnly, yOnly, twoDimensional }

public class PlayerMove : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField][Range(0.1f, 20f)] float playerSpeed = 3f;
    [SerializeField] [Range(1, 360)] int stepsPerMinute = 120;
    [SerializeField] moveMode moveDirections;
    [SerializeField] bool flipX;
    [SerializeField] bool flipY;
    [SerializeField] bool checkGround = true;
    [SerializeField] bool useAnimatorTurnTrigger = true;

    float lastAxis;

    PlayerGroundCheck gc;
    PlayerAnimControl ac;
    PlayerSFX sfx;

    float stepClock;

    private void OnEnable()
    {
        gc = GetComponent<PlayerGroundCheck>();
        ac = GetComponent<PlayerAnimControl>();
        sfx = GetComponent<PlayerSFX>();
        stepClock = 0f;
    }

    void FixedUpdate()
    {
        // If PlayerGroundCheck is on the same gameObject and checkGround is enabled, check if player is grounded
        //if (gc != null && checkGround && !gc.isGrounded) return;
        // Moves player depending on Input Axis
        transform.position += new Vector3(moveDirections != moveMode.yOnly ? Input.GetAxis("Horizontal") * (flipX ? -1 : 1) : 0, 0, moveDirections != moveMode.xOnly ? Input.GetAxis("Vertical") * (flipY ? -1 : 1) : 0) * playerSpeed * Time.deltaTime;
        // Updates AnimatorControl based on 1D movement
        if (ac != null) ac.MoveVector(moveDirections != moveMode.twoDimensional ? Input.GetAxis(moveDirections == moveMode.xOnly ? "Horizontal" : "Vertical") : 0);
    }

    void Update()
    {
        // Checks if PlayerSFX exists in GameObject
        if (sfx != null)
        {
            // If PlayerGroundCheck is on the same gameObject and checkGround is enabled, check if player is grounded
            if (gc != null && checkGround && !gc.isGrounded)
            {
                if (stepClock != 0f) stepClock = 0f;
                return;
            }

            if ((moveDirections != moveMode.yOnly ? Input.GetAxis("Horizontal") != 0f : true) && (moveDirections != moveMode.xOnly ? Input.GetAxis("Vertical") != 0f : true))
            {
                if (stepClock == 0f) sfx.Walk();
                stepClock += Time.deltaTime;
                if (stepClock >= (1f / ((float)stepsPerMinute / 60f)))
                {
                    stepClock = 0;
                }
            }
            else if (stepClock != 0f) stepClock = 0f;
        }
        // Attempts trigger of ac.Turn()
        if (moveDirections != moveMode.twoDimensional && ac != null && useAnimatorTurnTrigger)
        {
            if ((Input.GetAxis(moveDirections == moveMode.xOnly ? "Horizontal" : "Vertical") > 0 && lastAxis < 0) || (Input.GetAxis(moveDirections == moveMode.xOnly ? "Horizontal" : "Vertical") < 0 && lastAxis > 0)) ac.Turn();
        }
        // Sets lastAxis Value
        lastAxis = moveDirections == moveMode.twoDimensional ? 0 : Input.GetAxis(moveDirections == moveMode.xOnly ? "Horizontal" : "Vertical");
    }
}
