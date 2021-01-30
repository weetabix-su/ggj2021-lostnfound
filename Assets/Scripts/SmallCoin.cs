using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCoin : MonoBehaviour
{
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SmallCoins.instance.SpawnNext();
            Destroy(gameObject);
        }
    }
}
