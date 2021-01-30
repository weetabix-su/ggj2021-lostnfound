using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneral : MonoBehaviour
{
    public Transform snapPosition;

    private void OnEnable()
    {
        CheckPoint.OnCheck += SnapLocation;
    }

    private void OnDestroy()
    {
        CheckPoint.OnCheck -= SnapLocation;
    }

    void SnapLocation()
    {
        transform.position = snapPosition.position;
    }
}
