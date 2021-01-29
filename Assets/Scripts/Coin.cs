using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public MeshRenderer mr;
    public Rigidbody rb;
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        CoinCollider.OnEnterCoin += StartCoin;
    }

    private void OnDestroy()
    {
        CoinCollider.OnEnterCoin -= StartCoin;
    }

    void StartCoin()
    {
        mr.enabled = true;
        rb.useGravity = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LevelController.instance.LevelRetry();
            Destroy(this.gameObject);
        }
    }
}
