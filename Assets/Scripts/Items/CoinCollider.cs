using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    public delegate void EnterCoin();
    public static event EnterCoin OnEnterCoin;

    private void OnTriggerEnter(Collider other)
    {
        if (OnEnterCoin != null)
        {
            if(other.tag == "Player")
                OnEnterCoin();
        }
    }
}
