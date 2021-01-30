using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public Transform snapPosition;

    private void OnEnable()
    {
        SmallCoins.Picked += SnapLocation;
    }

    private void OnDestroy()
    {
        SmallCoins.Picked -= SnapLocation;
    }

    void SnapLocation()
    {
        transform.position = snapPosition.position;
    }
}
