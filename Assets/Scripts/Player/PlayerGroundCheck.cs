using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask groundLayers;
    [SerializeField] [Range(0.1f, 5f)] float detectDistance = 0.4f;
    [SerializeField] bool compareLayers = true;
    [SerializeField] bool compareHeights = true;
    public bool isGrounded;
    Collider col;

    public bool groundVerify(GameObject g)
    {
        return (groundLayers.value & 1 << g.layer) == 1 << g.layer && (compareHeights ? transform.position.y > g.transform.position.y : true);
    }

    void OnEnable()
    {
        isGrounded = false;
        col = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = ((compareLayers ? ((groundLayers.value & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer) : true) && (compareHeights ? transform.position.y > collision.transform.position.y : true));
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
