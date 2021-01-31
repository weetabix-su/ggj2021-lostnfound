using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBall : MonoBehaviour
{
    private bool actived = false;
    private Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.transform;
            actived = true;
        }
        if (other.tag == "DropDream")
        {
            Debug.Log("drop");
            actived = false;
            GetComponent<SphereCollider>().enabled = false;
            transform.position = new Vector3(player.position.x, player.position.y - 0.5f, player.position.z);
        }
    }

    private void Update()
    {
        if (actived && transform != null && player != null)
        {
            transform.position = new Vector3(player.position.x - 0.5f, player.position.y, player.position.z);
        }
    }
}
