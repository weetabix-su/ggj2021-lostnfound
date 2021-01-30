using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class God : MonoBehaviour
{
    public float movingDegree;

    public float localSpawnPosY;

    private void Awake()
    {
        localSpawnPosY = this.transform.localPosition.y;
    }

    public void Start()
    {
        GodMove(true);
    }

    public void GodMove(bool isUp)
    {
        if (isUp)
            LeanTween.moveLocalY(this.gameObject, localSpawnPosY + movingDegree, 2.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(()=> { GodMove(false); });
        else
            LeanTween.moveLocalY(this.gameObject, localSpawnPosY - movingDegree, 2.5f).setEase(LeanTweenType.easeOutSine).setOnComplete(() => { GodMove(true); });
    }

}
